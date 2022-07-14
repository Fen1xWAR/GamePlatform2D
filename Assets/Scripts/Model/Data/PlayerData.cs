using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public int Coins;
        public int MaxHp;
        public int Hp; // CurrentHP
        public int Damage;
        public int ThrowDamage;
        public bool CanAttack;
        public bool DoubleJump;
        public bool CanThrowAttack;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}

