using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement mov;
    private Ship ship;
    [SerializeField]private float _sensitivity = 0.5f;
    [SerializeField]private float _rotSensitivity = 3f;
    readonly float shootReset = 0.2f;
    private float shootTimer;

    private void Awake()
    {
        mov = GetComponent<Movement>();
        ship = transform.GetChild(0).GetComponent<Ship>();
        shootTimer = 0;
    }

    private void Update()
    {
        float yRot =  Input.GetAxis("Horizontal") * Time.deltaTime * _rotSensitivity;
        float xRot = Input.GetAxis("Vertical") * Time.deltaTime * _sensitivity;
        float zRot = Input.GetAxis("Spin") * Time.deltaTime * _rotSensitivity;
        float throttle = Input.GetAxis("Throttle") * Time.deltaTime;
        
        mov.RotatePivot(new Vector3(xRot, yRot, zRot));
        mov.RevampThrottle(throttle);
        
        float shoot = Input.GetAxis("Fire1");
        shootTimer -= Time.deltaTime;
        if (shoot > 0 && shootTimer <= 0)
        {
            ship.Fire();
            shootTimer = shootReset;
        }
    }
}
