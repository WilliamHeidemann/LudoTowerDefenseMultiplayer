using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Systems
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField] private Tower prefab;

        [SerializeField] private Territory redTerritory;
        [SerializeField] private Territory greenTerritory;
        [SerializeField] private Territory blueTerritory;
        [SerializeField] private Territory yellowTerritory;

        [SerializeField] private TeamColors teamColors;

        [SerializeField] private List<Transform> redTowerSpots;
        [SerializeField] private List<Transform> greenTowerSpots;
        [SerializeField] private List<Transform> blueTowerSpots;
        [SerializeField] private List<Transform> yellowTowerSpots;

        [SerializeField] private TowerConfiguration configuration;

        private List<Transform> Spots(Team team) =>
            team switch
            {
                Team.Red => redTowerSpots,
                Team.Green => greenTowerSpots,
                Team.Blue => blueTowerSpots,
                Team.Yellow => yellowTowerSpots,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

        public bool CanBuyTowers(Team team) => Spots(team).Any();

        public void SpawnTower(Team team)
        {
            var spots = Spots(team);
            if (!spots.Any()) return;
            var spot = spots.First();
            spots.RemoveAt(0);
            
            var tower = Instantiate(prefab, spot.position, Quaternion.identity);
            var territory = team switch
            {
                Team.Red => redTerritory,
                Team.Green => greenTerritory,
                Team.Blue => blueTerritory,
                Team.Yellow => yellowTerritory,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
            tower.territory = territory;
            tower.configuration = configuration;

            tower.GetComponent<Renderer>().material = team switch
            {
                Team.Red => teamColors.red,
                Team.Green => teamColors.green,
                Team.Blue => teamColors.blue,
                Team.Yellow => teamColors.yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha5)) SpawnTower(Team.Red);
            if (Input.GetKeyDown(KeyCode.Alpha6)) SpawnTower(Team.Green);
            if (Input.GetKeyDown(KeyCode.Alpha7)) SpawnTower(Team.Blue);
            if (Input.GetKeyDown(KeyCode.Alpha8)) SpawnTower(Team.Yellow);
        }
    }
}