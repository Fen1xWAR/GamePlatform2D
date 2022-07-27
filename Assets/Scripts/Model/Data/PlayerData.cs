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
        public int BaseDamage;
        public int Death;

        [Header("Player Level")]
        public int Level;
        public int Xp;
        public int XpToUp;
        public int AbilPoint;

        [Header("Specs")]
        public int ThrowDamage;
        public bool CanAttack;
        public bool DoubleJump;
        public bool WallSliding;
        public bool CanThrowAttack;
        public float CoinBonus;
        public int CoinLossPercent;

        [Header("Managment")]
        public string Scene;

        public PlayerData (Character player)
        {
            CurrentCheckpoint = player.CurrentCheckpoint;
            Coins = player.Coins;
            MaxHp = player.MaxHp;
            Hp = player.Hp;
            BaseDamage = player.BaseDamage;
            Death = player.Death;

            Level = player.Level;
            Xp = player.Xp;
            XpToUp = player.XpToUp;
            AbilPoint = player.AbilPoint;

            ThrowDamage = player.ThrowDamage;
            CanAttack = player.CanAttack;
            DoubleJump = player.DoubleJump;
            WallSliding = player.WallSliding;
            CanThrowAttack = player.CanThrowAttack;
            CoinBonus = player.CoinBonus;
            CoinLossPercent = player.CoinLossPercent;

            Scene = player.Scene;
        }
    }
}

