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
        [Header("Player Stats")]
        public int CurrentCheckpoint;
        public int Coins;
        public int MaxHp;
        public int Hp; // CurrentHP
        public int Damage;

        [Header("Player Level")]
        public int Level;
        public int Xp;
        public int XpToUp;

        [Header("Specs")]
        public int ThrowDamage;
        public bool CanAttack;
        public bool DoubleJump;
        public bool CanThrowAttack;

        [Header("Managment")]
        public string Scene;

        public PlayerData (Character player)
        {
            CurrentCheckpoint = player.CurrentCheckpoint;
            Coins = player.Coins;
            MaxHp = player.MaxHp;
            Hp = player.Hp;
            Damage = player.Damage;

            Level = player.Level;
            Xp = player.Xp;
            XpToUp = player.XpToUp;

            ThrowDamage = player.ThrowDamage;
            CanAttack = player.CanAttack;
            DoubleJump = player.DoubleJump;
            CanThrowAttack = player.CanThrowAttack;

            Scene = player.Scene;
        }
    }
}

