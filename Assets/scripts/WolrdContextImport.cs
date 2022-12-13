using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WorldContext", menuName = "World Context SO", order = 0)]
    public class WolrdContextImport : ScriptableObject
    {
        public int BaseLevel;
        public DangerLevel level;
    }
}