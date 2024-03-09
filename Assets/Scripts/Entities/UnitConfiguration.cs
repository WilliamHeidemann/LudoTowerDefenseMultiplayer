using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Unit", order = 0)]
    public class UnitConfiguration : ScriptableObject
    {
        public int startingHealth;
        public float moveSpeed;
    }
}