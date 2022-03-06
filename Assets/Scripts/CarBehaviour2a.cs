/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/


//not changed yet


using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour
{

    void LateUpdate()
    {
        // YOUR CODE HERE
        float leftSensor = 0, rightSensor = 0;

        //Read sensor values
        if (DetectLights)
        {
            leftSensor = LeftLD.GetOutput();
            rightSensor = RightLD.GetOutput();
        }

        if (DetectCars)
        {
            leftSensor = LeftCD.GetOutput();
            rightSensor = RightCD.GetOutput();
        }


        //Calculate target motor values
        m_LeftWheelSpeed = leftSensor * MaxSpeed;
        m_RightWheelSpeed = rightSensor * MaxSpeed;
    }
}
