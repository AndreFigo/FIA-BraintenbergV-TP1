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
        bool [] areActive = { DetectLights, DetectCars, DetectBumperLights };
        DetectorScript[] sensorRight = { RightLD, RightCD, RightBLD};
        DetectorScript[] sensorLeft = { LeftLD, LeftCD, LeftBLD };
        //Read sensor values
        int noActiveSensors=0;
        for(int i =0; i<areActive.Length; i++)
        {
            if(areActive[i])
            {
                noActiveSensors++;
                leftSensor += sensorLeft[i].GetOutput();
                rightSensor += sensorRight[i].GetOutput();
            }
        }
        leftSensor /= (noActiveSensors + 0.0f);
        rightSensor /= (noActiveSensors + 0.0f);


        //Calculate target motor values
        m_LeftWheelSpeed = leftSensor * MaxSpeed;
        m_RightWheelSpeed = rightSensor * MaxSpeed;
    }
}
