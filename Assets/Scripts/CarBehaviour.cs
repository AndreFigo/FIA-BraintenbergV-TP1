﻿/* 	FCTUC - LEI - AIF - PROJECT 1
	André Guilherme Gonçalves Figo de Carvalho, nº 2019216156
	Paulo Miguel Teixeira Cortesão, nº 2019216517
*/


//not changed yet


using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour
{

    public float MaxSpeed;
    public WheelCollider RR;
    public WheelCollider RL;
    public bool DetectLights = true;
    public bool DetectCars = false;
    public bool DetectBumperLights = false;
    public LightDetectorScript RightLD;
    public LightDetectorScript LeftLD;
    public CarDetectorScript LeftCD;
    public CarDetectorScript RightCD;
    public BumperLightDetectorScript RightBLD;
    public BumperLightDetectorScript LeftBLD;



    private Rigidbody m_Rigidbody;
    public float m_LeftWheelSpeed;
    public float m_RightWheelSpeed;
    private float m_axleLength;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.freezeRotation = true;
        m_axleLength = (RR.transform.position - RL.transform.position).magnitude;
    }

    void FixedUpdate()
    {
        //Calculate forward movement
        float targetSpeed = (m_LeftWheelSpeed + m_RightWheelSpeed) / 2;
        Vector3 movement = transform.forward * targetSpeed * Time.fixedDeltaTime;

        //Calculate turn degrees based on wheel speed
        float angVelocity = (m_LeftWheelSpeed - m_RightWheelSpeed) / m_axleLength * Mathf.Rad2Deg * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, angVelocity, 0.0f);

        //Apply to rigid body
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
