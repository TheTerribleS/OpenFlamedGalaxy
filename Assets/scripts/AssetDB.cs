using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Assets", menuName = "asset database", order = 0)]
    public class AssetDB : ScriptableObject
    {
        public GameObject[] asteroids;
        public GameObject[] HumanShips, ReptileShips, HNoidShips, HybShips, RebelShips;

       
    }
}