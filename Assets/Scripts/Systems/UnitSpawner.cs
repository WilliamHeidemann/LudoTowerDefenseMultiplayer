using System;
using Entities;
using UnityEngine;

namespace Systems
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Unit prefab;
        
        public void SpawnUnit(Team team)
        {
            var unit = Instantiate(prefab);
            unit.team = team;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnUnit(Team.Red);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnUnit(Team.Green);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnUnit(Team.Blue);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SpawnUnit(Team.Yellow);
        }
    }
}