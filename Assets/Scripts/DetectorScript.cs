using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DetectorScript : MonoBehaviour
{
    public bool invertOutput = false;
    // Start is called before the first frame update
    void Start() { }
    // Update is called once per frame
    void Update() { }
    public virtual float FuncOutput(float output)
    {
        throw new NotImplementedException("Not implemented");
    }
    public virtual float GetOutput()
    {
        throw new NotImplementedException("Not implemented");
    }
}
