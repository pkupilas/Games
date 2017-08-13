﻿using UnityEngine;

namespace MoneyBox
{
    public class MoneyBox : MonoBehaviour
    {
        [SerializeField] private float _playerMoney;

        public float GetPlayerMoney()
        {
            return _playerMoney;
        }

        public void PayForSpin(float rollCost)
        {
            _playerMoney -= rollCost;
        }

        public void AddMoney(float money)
        {
            _playerMoney += money;
        }
    }
}
