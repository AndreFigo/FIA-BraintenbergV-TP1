/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/
using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class CarDetectorScript : DetectorScript
{

    public float angle = 360;
    public bool ApplyThresholds, ApplyLimits;
    public float MinX, MaxX, MinY, MaxY;
    public bool useAngle = true;

    public bool interactAll = false, closestCar = false, chaser = false;

    public float output;
    public int numObjects;
    public int has_own = 0;

    void Start()
    {
        output = 0;
        numObjects = 0;

        if (angle > 360)
        {
            useAngle = false;
        }
    }

    void Update()
    {
        GameObject[] carsToInteract = null;
        if (interactAll)
            carsToInteract = GetAllCars();
        else if (closestCar)
            carsToInteract = new GameObject[] { GetClosestCar() };
        else
            carsToInteract = GetAllCarsToFollow();

        // if there is a car to follow calculate the energy to output according to the distance to that car
        output = 0;
        has_own = 0;
        Debug.Log("Cars to interact length: " + carsToInteract.Length);
        foreach (var c in carsToInteract)
        {
            var distance = (c.transform.position - transform.position).magnitude;
            // 1.2 is a safe distance between the wheels and the center of the car
            // this is used to prevent a car from pursuing itself
            if (!chaser && distance < 1.2)
            {
                has_own = 1;
                Debug.Log("Entrou");
                continue;
            }
            float r = 10;
            output += (1.0f / ((c.transform.position - transform.position).sqrMagnitude / r + 1));
        }
        if (carsToInteract.Length > has_own)
            output /= (carsToInteract.Length - has_own);
    }

    GameObject GetClosestCar()
    {
        GameObject closestCar = null;
        //gets all objects with the tag "CarToFollow"
        var cars = GetAllCarsToFollow();

        //calculates the distance between the current car and the other cars in order to find the closest
        foreach (var c in cars)
        {
            // Console.WriteLine(distance)
            var distance = (c.transform.position - transform.position).magnitude;
            if (closestCar == null || (distance < (closestCar.transform.position - transform.position).magnitude && (distance > 1.2 || chaser)))
            {
                closestCar = c;
            }
        }
        return closestCar;
    }

    public override float GetOutput()
    {
        float minVal = 0, maxVal = float.MaxValue;
        //y axis
        if (ApplyLimits)
        {
            minVal = MinY;
            maxVal = MaxY;
        }
        //X axis
        if (ApplyThresholds && (output < MinX || output > MaxX))
        {
            return 0f;
        }
        float f_out = FuncOutput(output);
        if (f_out < minVal) return minVal;
        if (f_out > maxVal) return maxVal;

        return f_out;
    }

    // Returns all "CarToFollow" tagged objects. The sensor angle is not taken into account.
    GameObject[] GetAllCarsToFollow()
    {
        return GameObject.FindGameObjectsWithTag("CarToFollow");
    }

    GameObject[] GetAllCars()
    {
        var carsToFollow = GameObject.FindGameObjectsWithTag("CarToFollow");
        var chaserCars = GameObject.FindGameObjectsWithTag("CarChaser");
        return carsToFollow.Concat(chaserCars).ToArray();
    }




}
