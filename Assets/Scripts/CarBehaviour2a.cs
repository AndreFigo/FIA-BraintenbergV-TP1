/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/


//not changed yet


using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour
{

    public float[] coefficients = { 0.25f, 0.1f, 0.75f };
    public float intensityFactor = 3;

    private const int RND_LIMIT = 100000;
    private const float GROUND_SIZE = 10.0f;
    void LateUpdate()
    {
        // YOUR CODE HERE
        float leftSensor = 0, rightSensor = 0;
        bool[] areActive = { DetectLights, DetectCars, DetectBumperLights };
        DetectorScript[] sensorRight = { RightLD, RightCD, RightBLD };
        DetectorScript[] sensorLeft = { LeftLD, LeftCD, LeftBLD };
        //Read sensor values
        int noActiveSensors = 0;
        for (int i = 0; i < areActive.Length; i++)
        {
            if (areActive[i])
            {
                noActiveSensors++;
                leftSensor += intensityFactor * coefficients[i] * sensorLeft[i].GetOutput();
                rightSensor += intensityFactor * coefficients[i] * sensorRight[i].GetOutput();
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
            //other.gameObject.SetActive(false);
            //change to a random position

            GameObject ground = GameObject.FindGameObjectsWithTag("Ground")[0];
            float scaleX = ground.transform.localScale[0];
            float scaleZ = ground.transform.localScale[2];
            float xMin = ground.transform.position[0] - GROUND_SIZE / 2 * scaleX;
            float deltaX = GROUND_SIZE * ground.transform.localScale[0];
            float zMin = ground.transform.position[2] - GROUND_SIZE / 2 * scaleZ;
            float deltaZ = GROUND_SIZE * ground.transform.localScale[2];
            float x, z;
            x = Random.Range(xMin + 5, xMin + deltaX - 5);
            z = Random.Range(zMin + 5, zMin + deltaZ - 5);
            other.gameObject.transform.SetPositionAndRotation(new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0));
        }
    }
}
