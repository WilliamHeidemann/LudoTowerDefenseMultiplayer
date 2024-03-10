using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Systems;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using View;

public class Economy : NetworkBehaviour
{
    private Player player;
    private Shop shop;
    [SerializeField] private TextMeshProUGUI moneyText;
    private Dictionary<ulong, int> bank;

    public void Init()
    {
        player = new Player(moneyText);
        shop = new Shop();
    }

    private void Update()
    {
        if (IsHost) 
            if(Input.GetKeyDown(KeyCode.Space)) 
                PayPlayersRpc(100);
    }

    public override void OnNetworkSpawn()
    {
        if (IsHost) bank = new Dictionary<ulong, int>();
        print(NetworkManager.LocalClientId);
        ConnectToBankRpc(NetworkManager.LocalClientId);
    }

    [Rpc(SendTo.Server)] private void ConnectToBankRpc(ulong clientId) => bank.Add(clientId, player.Money);

    private class Player
    {
        private readonly TextMeshProUGUI moneyText;
        private int money;

        public int Money
        {
            get => money;
            set
            {
                money = value;
                moneyText.text = money.ToString();
            }
        }
        public Player(TextMeshProUGUI moneyText)
        {
            this.moneyText = moneyText;
            Money = 100;
        }
    }

    private class Shop
    {
        public readonly UnitSpawner UnitSpawner = FindFirstObjectByType<UnitSpawner>();
        public readonly TowerSpawner TowerSpawner = FindFirstObjectByType<TowerSpawner>();
    }
    public void RequestUnit() => 
        BuyUnitRpc(NetworkManager.LocalClientId);
    
    [Rpc(SendTo.Server)]
    private void BuyUnitRpc(ulong clientId)
    {
        const int unitCost = 10;
        if (bank[clientId] < unitCost) return;
        var team = TeamLookup.Get(clientId);
        SpawnUnitRpc(team);
        bank[clientId] -= unitCost;
        DeductMoneyRpc(unitCost, clientId);
    }
    [Rpc(SendTo.ClientsAndHost)] private void DeductMoneyRpc(int amount, ulong clientId)
    {
        if (clientId != NetworkManager.LocalClientId) return;
        player.Money -= amount;
    }

    [Rpc(SendTo.ClientsAndHost)] private void SpawnUnitRpc(Team team) => 
        shop.UnitSpawner.SpawnUnit(team);
    public void RequestTower() => 
        BuyTowerRpc(NetworkManager.LocalClientId);
    
    [Rpc(SendTo.Server)]
    private void BuyTowerRpc(ulong clientId)
    {
        const int towerCost = 100;
        if (bank[clientId] < towerCost) return;
        var team = TeamLookup.Get(clientId);
        if (!shop.TowerSpawner.CanBuyTowers(team)) return;
        SpawnTowerRpc(team);
        bank[clientId] -= towerCost;
        DeductMoneyRpc(towerCost, clientId);
    }
    [Rpc(SendTo.ClientsAndHost)] private void SpawnTowerRpc(Team team) => 
        shop.TowerSpawner.SpawnTower(team);

    [Rpc(SendTo.ClientsAndHost)]
    public void PayPlayersRpc(int amount)
    {
        player.Money += amount;
        if (IsHost) foreach (var playerId in bank.Keys.ToList()) bank[playerId] += amount;
    }
}