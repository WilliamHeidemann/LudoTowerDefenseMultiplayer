using System;
using System.Collections.Generic;
using System.Linq;
using MyUtility;
using UnityEngine;

namespace Entities
{
    public class Territory : MonoBehaviour
    {
        public Team toExclude;
        private readonly LinkedList<Unit> entrants = new();
        private void Enter(Unit unit)
        {
            entrants.AddFirst(unit);
            unit.OnDeath += Exit;
        }

        private void Exit(Unit unit)
        {
            entrants.Remove(unit);
            unit.OnDeath -= Exit;
        }
        
        public Option<Unit> Furthest()
        {
            var last = entrants.LastOrDefault(unit => unit.team != toExclude);
            return last == null ? Option<Unit>.None : Option<Unit>.Some(last);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Unit>(out var unit))
            {
                if (unit.team != toExclude) Enter(unit);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Unit>(out var unit))
            {
                if (unit.team != toExclude) Exit(unit);
            }
        }
    }
}