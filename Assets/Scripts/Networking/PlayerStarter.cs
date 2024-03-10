using Entities;
using Unity.Netcode;
using UnityEngine;

namespace Networking
{
    public class PlayerStarter : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner) FindFirstObjectByType<Economy>().Init();
        }
    }
}