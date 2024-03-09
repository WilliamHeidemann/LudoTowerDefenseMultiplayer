using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Systems
{
    public class WayPointSystem : MonoBehaviour
    {
        [SerializeField] private List<Transform> wayPoints;
        
        [SerializeField] private Transform startingPointRed;
        [SerializeField] private Transform startingPointGreen;
        [SerializeField] private Transform startingPointBlue;
        [SerializeField] private Transform startingPointYellow;
        
        [SerializeField] private Transform endPointRed;
        [SerializeField] private Transform endPointGreen;
        [SerializeField] private Transform endPointBlue;
        [SerializeField] private Transform endPointYellow;

        private int startingIndexRed;
        private int startingIndexGreen;
        private int startingIndexBlue;
        private int startingIndexYellow;

        private readonly Dictionary<WayPointChaser, int> index = new();
        private readonly Dictionary<WayPointChaser, int> pointsReached = new();

        public void StartChase(WayPointChaser chaser, Team team)
        {
            var targetTransform = team switch
            {
                Team.Red => startingPointRed,
                Team.Green => startingPointGreen,
                Team.Blue => startingPointBlue,
                Team.Yellow => startingPointYellow,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

            var startingIndex = team switch
            {
                Team.Red => 0 * (wayPoints.Count / 4),
                Team.Green => 1 * (wayPoints.Count / 4),
                Team.Blue => 2 * (wayPoints.Count / 4),
                Team.Yellow => 3 * (wayPoints.Count / 4),
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

            chaser.EndPoint = team switch
            {
                Team.Red => endPointRed.position,
                Team.Green => endPointGreen.position,
                Team.Blue => endPointBlue.position,
                Team.Yellow => endPointYellow.position,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

            index[chaser] = startingIndex;
            pointsReached[chaser] = 0;
            chaser.Target = wayPoints[startingIndex].position;
            chaser.transform.position = targetTransform.position;
            chaser.TargetReached += AssignNextTarget;
        }

        private void AssignNextTarget(WayPointChaser chaser)
        {
            index[chaser]++;
            pointsReached[chaser]++;
            chaser.Target = wayPoints[index[chaser] % wayPoints.Count].position;
            if (pointsReached[chaser] == wayPoints.Count)
            {
                chaser.TargetReached -= AssignNextTarget;
                chaser.Target = chaser.EndPoint;
            }
        }
    }
}