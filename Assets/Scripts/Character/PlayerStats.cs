using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class PlayerStats : MonoBehaviour
    {
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
        public float[] Position;
    }
}