using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class BumperLightDetectorGaussScript : BumperLightDetectorScript

{
    public float stdDev = 1.0f;
    public float mean = 0.0f;
    public float min_y;
    public bool inverse = false;
    // Get gaussian output value
    public override float FuncOutput(float output)
    {
        // YOUR CODE HERE
        return (float)(1.0f / (Math.Sqrt(2 * Math.PI) * stdDev) * Math.Exp(-0.5f * Math.Pow((output - mean) / stdDev, 2)));
    }
}
