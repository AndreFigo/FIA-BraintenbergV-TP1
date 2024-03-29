using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BumperLightDetectorScript : DetectorScript
{



    public float angle = 360;
    public bool ApplyThresholds, ApplyLimits;
    public float MinX, MaxX, MinY, MaxY;
    private bool useAngle = true;
    public bool useClosest = true;


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
        GameObject[] bumperLights;

        if (useClosest)
        {
            bumperLights = new GameObject[1];
            bumperLights[0] = GetClosestBumperLight();
        }
        else
        {
            bumperLights = GetAllBumperLights();
        }

        output = 0.0f;
        numObjects = bumperLights.Length;

        foreach (GameObject bl in bumperLights)
        {
            //print (1 / (transform.position - light.transform.position).sqrMagnitude);
            float r = bl.GetComponent<Light>().range;
            output += (1.0f / ((transform.position - bl.transform.position).sqrMagnitude / (4 * r) + 1));
            //Debug.DrawLine (transform.position, light.transform.position, Color.red);
        }
        if (numObjects > 0)
            output /= numObjects;

    }
    //public virtual float GetOutput() { throw new NotImplementedException("Not implemented"); }
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
        // Console.WriteLine(f_out);
        if (f_out < minVal && ApplyLimits) return minVal;
        if (f_out > maxVal && ApplyLimits) return maxVal;

        return f_out;
    }

    // Returns all "Light" tagged objects. The sensor angle is not taken into account.
    GameObject[] GetAllBumperLights()
    {
        return GameObject.FindGameObjectsWithTag("Pickable");
    }

    GameObject GetClosestBumperLight()
    {
        GameObject closestLight = null;
        //gets all objects with the tag "CarToFollow"
        var bLights = GetAllBumperLights();

        //calculates the distance between the current car and the other cars in order to find the closest
        foreach (var b in bLights)
        {
            if (closestLight == null || (b.transform.position - transform.position).magnitude < (closestLight.transform.position - transform.position).magnitude)
            {
                closestLight = b;
            }
        }
        return closestLight;
    }
    // Returns all "Light" tagged objects that are within the view angle of the Sensor. 
    // Only considers the angle over the y axis. Does not consider objects blocking the view.
    GameObject[] GetVisibleBumperLights()
    {
        ArrayList visibleLights = new ArrayList();
        float halfAngle = angle / 2.0f;

        GameObject[] lights = GameObject.FindGameObjectsWithTag("Pickable");

        foreach (GameObject light in lights)
        {
            Vector3 toVector = (light.transform.position - transform.position);
            Vector3 forward = transform.forward;
            toVector.y = 0;
            forward.y = 0;
            float angleToTarget = Vector3.Angle(forward, toVector);

            if (angleToTarget <= halfAngle)
            {
                visibleLights.Add(light);
            }
        }

        return (GameObject[])visibleLights.ToArray(typeof(GameObject));
    }

}
