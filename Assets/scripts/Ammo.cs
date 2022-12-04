using System;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Ammo : MonoBehaviour
{
    [SerializeField] int damage;
    private float timer;
    [SerializeField]private float timerReset = 5;
    public bool isUsing;
    [SerializeField] private float speed = 20;
    private Ship shooter;
    private Vector3 savedForward = Vector3.zero;

    public int Damage => damage;
    
    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else Deactivate();
        transform.Translate(savedForward * (speed * Time.deltaTime));
    }

    public void Use(Ship ship)
    {
        shooter = ship;
        Transform trans = ship.transform;
        savedForward = ship.transform.forward;
        transform.position = trans.position + trans.forward * 10;
        timer = timerReset;
        isUsing = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        transform.rotation = new Quaternion();
        isUsing = false;
        gameObject.SetActive(false);
    }
}
