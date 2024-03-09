using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Tower Config", menuName = "Tower", order = 0)]
    public class TowerConfiguration : ScriptableObject
    {
        public int damage;
        public float fireRate;
    }
}