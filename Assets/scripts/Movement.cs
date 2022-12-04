using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Movement : MonoBehaviour
{
    [SerializeField]float speed;
    private float _power;

    [SerializeField] private GameObject forwardVis;
    void Start()
    {
        _power = 1;
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * _power * Time.deltaTime), Space.Self);
        forwardVis.transform.position = transform.position + transform.forward * ( speed * _power);
    }

    public void RevampThrottle(float newPow)
    {
        _power += newPow;
        if (_power < 0) _power = 0;
        if (_power > 1) _power = 1;
    }

    public void RotatePivot(Vector3 rotType)
    {
        transform.Rotate(rotType, Space.Self);
    }

}


