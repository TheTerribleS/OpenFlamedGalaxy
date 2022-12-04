using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]Factions faction;
    [SerializeField]private int _health = 75;
    public UIDot myDot;
    public Role role;

    public List<Item> Cargo;

    public Factions Faction => faction;

    private void Awake()
    {
        transform.GetChild(0).AddComponent<ShipRef>().mainRef = this;
        foreach (Transform child in transform.GetChild(0))
        {
            child.AddComponent<ShipRef>().mainRef = this;
        }
    }

    private void Start()
    {
        WorldContext.Instance.AddShip(this);
       
    }

    public void ReceiveDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            WorldContext.Instance.DeleteShip(this);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (myDot != null)
            myDot.Render = false;
    }

    void OnBecameVisible()
    {
        if (myDot != null)
            myDot.Render = true;
    }

    public void Fire()
    {
        WorldContext.Instance.UseAmmo(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Ammo ammo = collision.gameObject.GetComponent<Ammo>();
            ReceiveDamage(ammo.Damage);
        }
    }
}