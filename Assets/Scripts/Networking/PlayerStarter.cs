using Entities;
using Unity.Netcode;
using UnityEngine;

namespace Networking
{
    public class PlayerStarter : NetworkBehaviour
    {
        [SerializeField] private Economy economyPrefab;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner) SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var economy = Instantiate(economyPrefab);
            economy.team = Team.Blue;
        }
    }
}