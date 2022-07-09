using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int MaxHp;
        public int Hp; // CurrentHP
        public int Damage;
        public int ThrowDamage;
        public bool CanAttack;
        public bool DoubleJump;
        public bool CanThrowAttack;
    }
}

