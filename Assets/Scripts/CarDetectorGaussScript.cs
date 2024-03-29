﻿/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/


//not changed yet


using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class CarDetectorGaussScript : CarDetectorScript
{

    public float stdDev = 1.0f;
    public float mean = 0.0f;
    // Get gaussian output value
    public override float FuncOutput(float output)
    {
        // YOUR CODE HERE
        float ans = (float)(1.0f / (Math.Sqrt(2 * Math.PI) * stdDev) * Math.Exp(-0.5f * Math.Pow((output - mean) / stdDev, 2)));
        return (invertOutput ? -ans : ans);
    }


}
