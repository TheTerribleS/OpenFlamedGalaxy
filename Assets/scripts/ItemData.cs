using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 0, fileName = "ItemData", menuName = "Item data")]
public class ItemData : ScriptableObject
{
    public List<ItemStaticData> data = new List<ItemStaticData>();
}

[System.Serializable]
public class ItemStaticData
{
    public readonly int id;
    public readonly string name;
    public readonly float baseValue;
}
