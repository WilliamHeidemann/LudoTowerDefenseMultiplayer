using System;
using TMPro;
using UnityEngine;

namespace View
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        private static MoneyCounter moneyCounter;
        private void Awake() => moneyCounter = this;
        public static void UpdateMoneyText(string text) => moneyCounter.moneyText.text = text;
    }
}