using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class UIDot
{
    private Ship shipToFollow;

    public Image dot;

    private bool render;

    public bool Render
    {
        get { return render; }
        set
        {
            render = value;
            if (dot == null) return;
            dot.gameObject.SetActive(value);
        }
    }

    public void SetShip(Ship ship)
    {
        shipToFollow = ship;
        shipToFollow.myDot = this;
    }

    public void Update()
    {
        if (render) 
            dot.transform.position = GetScreenPos();
    }

    public Vector2 GetScreenPos()
    {
        if (!render) return Vector2.one * -1;

        if (shipToFollow && Camera.main) 
            return Camera.main.WorldToScreenPoint(shipToFollow.transform.position);
        
        return Vector2.one * -1;
    }
    
    
}
