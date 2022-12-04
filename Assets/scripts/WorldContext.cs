using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WorldContext : MonoBehaviour
{
    public static WorldContext Instance;
    [SerializeField] private GameObject AmmoGo;
    [SerializeField] private Factions  faction;
    [SerializeField] private DangerLevel danger;
    [SerializeField] private int   stationLevel;
    [SerializeField] private Vector3[] _genZone = new Vector3[2];
    public Station station;

    public Factions Faction => faction;
    public DangerLevel Danger => danger;
    public Tuple<Vector3, Vector3> GenZone => new(_genZone[0], _genZone[1]);
    public int Level => stationLevel;

    List<Ship> ships = new List<Ship>();

    private List<Ammo> _ammo = new();

    public delegate void ShipEvents(Ship ship);

    public static ShipEvents ShipDeath, ShipSpawn;


    private void Awake()
    {
        if (!Instance) Instance = this;
        else Debug.LogError("More than 1 WorldContext exist in the scene!");
        for (int i = 0; i < 40; i++)
        {
            _ammo.Add(Instantiate(AmmoGo, transform).GetComponent<Ammo>());
            _ammo[i].Deactivate();
        }
        
    }

    public void AddShip(Ship ship)
    {
        ships.Add(ship);
        ShipSpawn.Invoke(ship);
    }

    public void DeleteShip(Ship ship)
    {
        ships.Remove(ship);
        ShipDeath.Invoke(ship);
    }

    public void UseAmmo(Ship ship)
    {
        Ammo amm;
        for (int i = 0; i < _ammo.Count; i++)
        {
            if (_ammo[i].isUsing) continue;
            _ammo[i].Use(ship);
            return;
        }
        Instantiate(AmmoGo).GetComponent<Ammo>().Use(ship);
    }
}
