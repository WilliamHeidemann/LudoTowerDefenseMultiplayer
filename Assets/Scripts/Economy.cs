using System;
using Entities;
using Systems;
using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public Team team;
    [SerializeField] private TextMeshProUGUI moneyText;
    private Player player;
    private Shop shop;

    private void Start()
    {
        player = new Player(team, moneyText);
        shop = new Shop();
    }
    
    private class Player
    {
        private int money;
        public int Money
        {
            get => money;
            set
            {
                money = value;
                moneyText.text = $"${money}";
            }
        }
        public readonly Team Team;
        private readonly TextMeshProUGUI moneyText;

        public Player(Team team, TextMeshProUGUI moneyText)
        {
            Team = team;
            this.moneyText = moneyText;
            Money = 100;
        }
    }

    private class Shop
    {
        public readonly UnitSpawner UnitSpawner = FindFirstObjectByType<UnitSpawner>();
        public readonly TowerSpawner TowerSpawner = FindFirstObjectByType<TowerSpawner>();
    }
    
    public void BuyUnit()
    {
        const int unitCost = 10;
        if (player.Money >= unitCost)
        {
            shop.UnitSpawner.SpawnUnit(player.Team);
            player.Money -= unitCost;
        }
    }

    public void BuyTower()
    {
        const int towerCost = 100;
        if (player.Money >= towerCost)
        {
            shop.TowerSpawner.SpawnTower(player.Team);
            player.Money -= towerCost;
        }
    }

    public void PayPlayer(int amount)
    {
        player.Money += amount;
    }
}