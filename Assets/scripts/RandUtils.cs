using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class RandUtils : MonoBehaviour
    {
        public static Vector3 GetRandomVector(Tuple<Vector3, Vector3> zone)
        {
            return new Vector3(Random.Range(zone.Item1.x, zone.Item2.x),
                               Random.Range(zone.Item1.y, zone.Item2.y),
                               Random.Range(zone.Item1.z, zone.Item2.z));
        }
    }
}