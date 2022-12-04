using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Color normal, ally, enemie;
    private List<UIDot> dots = new();
    [SerializeField] private Ship playerShip;
    private void Awake()
    {
        WorldContext.ShipSpawn += AddShip;
            WorldContext.ShipDeath += CheckForShip;
    }

    void AddShip(Ship ship)
    {
        if (ship == playerShip) return;



        UIDot dot = new UIDot
        {
            dot = Instantiate(dotPrefab, transform).GetComponent<Image>()
        };
        dot.dot.color = GetSideColor(ship);
        dot.SetShip(ship);
        dots.Add(dot);
    }
    
    void CheckForShip(Ship ship)
    {
        
    }

    private void Update()
    {
        foreach (var dot in dots)
        {
            if (!dot.Render) continue;
            dot.Update();
        }
    }

    private Color GetSideColor(Ship ship)
    {
        if (ship.Faction is Factions.Enemy or Factions.Rebel) 
            return enemie;

        if (ship.Faction == playerShip.Faction)
        {
            return ally;
        }

        return normal;
    }
}
