using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    
}

[Serializable]
public enum Factions

{
    Human,
    Reptile,
    Humanoid,
    Hybrid,
    Rebel,
    Enemy 
}

public enum DangerLevel
{
    Safe,
    Medium,
    Caution,
    Deadly
}

[Serializable]
public enum Role
{
    Security,
    Mercenary,
    Miner,
    Merchant,
    Escort
}

public class Item
{
    public string name;
    public int id;
    public int tons;
}

