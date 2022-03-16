/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/


//not changed yet


using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour
{
    
    public float[] coefficients = {0.25f,0f,0.75f};
    public float intensityFactor=3;
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
                leftSensor += intensityFactor* coefficients[i]*sensorLeft[i].GetOutput();
                rightSensor += intensityFactor*coefficients[i]*sensorRight[i].GetOutput();
            }
        }
        //Calculate target motor values
        m_LeftWheelSpeed = leftSensor * MaxSpeed;
        m_RightWheelSpeed = rightSensor * MaxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
