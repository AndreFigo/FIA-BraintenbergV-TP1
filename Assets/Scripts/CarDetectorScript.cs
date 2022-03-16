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
    private bool useAngle = true;

    
    public float output;
    public int numObjects;

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
        GameObject closestCar = null;
        if (useAngle)
            closestCar = GetClosestCar();
        else
            closestCar = null;

        // if there is a car to follow calculate the energy to output according to the distance to that car
        if (closestCar)
        {
            // the expression is supposed to mimic (pursuit or love)/exploratory behaviour of a Braitenberg vehicle, depending on how the sensors are positioned
            float r = 10;
            output = 1.0f - (1.0f / ((closestCar.transform.position - transform.position).sqrMagnitude / r + 1));
        }
        else
            output = 0;


    }

    GameObject GetClosestCar()
    {
        GameObject closestCar = null;
        //gets all objects with the tag "CarToFollow"
        var cars = GetAllCars();

        //calculates the distance between the current car and the other cars in order to find the closest
        foreach (var c in cars)
        {
            if (closestCar == null || (c.transform.position - transform.position).magnitude < (closestCar.transform.position - transform.position).magnitude)
            {
                closestCar = c;
            }
        }
        return closestCar;
    }
        
    public override float GetOutput() { 
        float minVal=0, maxVal=float.MaxValue;
        //y axis
        if(ApplyLimits) {
            minVal = MinY;
            maxVal = MaxY;
        }
        //X axis
        if (ApplyThresholds && (output < MinX || output > MaxX)) {
            return 0f;
        }
        float f_out = FuncOutput(output);
        if(f_out < minVal) return minVal;
        if(f_out > maxVal) return maxVal;

        return f_out;            
    }

    // Returns all "CarToFollow" tagged objects. The sensor angle is not taken into account.
    GameObject[] GetAllCars()
    {
        return GameObject.FindGameObjectsWithTag("CarToFollow");
    }




}
