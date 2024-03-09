using System;
using Entities;
using UnityEngine;

namespace Systems
{
    // Eventuelt tilføje senere at en unit hopper 1-6 felt frem, venter lidt og så hopper igen.
    // De bliver også slået hjem hvis en anden unit hopper til deres felt. 
    // Og hvis to fra samme team stå på et felt, bliver den fremmede unit slået hjem.
    public class WayPointChaser : MonoBehaviour
    {
        public Vector3 Target { get; set; }
        public Vector3 EndPoint { get; set; }
        public event Action<WayPointChaser> TargetReached;
        private Transform cacheTransform;
        private float moveSpeed;

        private void Awake()
        {
            cacheTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            var unit = GetComponent<Unit>();
            moveSpeed = unit.configuration.moveSpeed;
            FindAnyObjectByType<WayPointSystem>().StartChase(this, unit.team);
        }

        private void Update()
        {
            cacheTransform.position = Vector3.MoveTowards(cacheTransform.position, Target, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(cacheTransform.position, Target) < 0.1f) TargetReached?.Invoke(this);
        }
    }
}