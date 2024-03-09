using System;
using UnityEngine;

namespace Entities
{
    public class Tower : MonoBehaviour
    {
        public TowerConfiguration configuration;
        public Territory territory;

        private void Start()
        {
            StartFiring();
        }

        private async void StartFiring()
        {
            while (true)
            {
                await Awaitable.WaitForSecondsAsync(configuration.fireRate);
                if (territory.Furthest().IsSome(out var target))
                {
                    target.OnDeath += Reward;
                    FireAnimation();
                    target.TakeDamage(configuration.damage);
                    target.OnDeath -= Reward;
                }
            }
        }
        
        void Reward(Unit unit){}

        private void FireAnimation()
        {
        }
    }
}