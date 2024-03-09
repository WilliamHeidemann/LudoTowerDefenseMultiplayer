using System;
using TMPro;
using UnityEngine;

namespace View
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        private static MoneyCounter singleton;
        private void Awake() => singleton = this;

        public static void UpdateMoneyText(string text) => singleton.moneyText.text = text;
    }
}