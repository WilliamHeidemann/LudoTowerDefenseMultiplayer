using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class Unit : MonoBehaviour
    {
        public Team team;
        public UnitConfiguration configuration;
        public int health;
        public Action<Unit> OnDeath;
        [SerializeField] private TeamColors teamColors;
        [SerializeField] private Slider healthBar;

        private void Awake()
        {
            health = configuration.startingHealth;
            OnDeath += Die;
        }

        private void Start()
        {
            ApplyMaterial();
        }

        private void ApplyMaterial()
        {
            GetComponent<Renderer>().material = team switch
            {
                Team.Red => teamColors.red,
                Team.Green => teamColors.green,
                Team.Blue => teamColors.blue,
                Team.Yellow => teamColors.yellow,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            healthBar.value = health / (float)configuration.startingHealth;         
            if (health <= 0)
            {
                OnDeath?.Invoke(this);
            }
        }
        
        private void Die(Unit unit) => Destroy(gameObject);
    }
}