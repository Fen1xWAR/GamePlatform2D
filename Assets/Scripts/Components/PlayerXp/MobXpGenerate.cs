using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MobXpGenerate : MonoBehaviour
    {
        private Character _character;
        [SerializeField] private GameObject Character;
        [SerializeField] private int _xpAmount;
        [SerializeField] private float _xpMultiply = 1f;

        private void Start()
        {
            _character = GetComponent<Character>();
            Character = GameObject.FindWithTag("Player");
        }
        public void OnDeathGainXp()
        {
            Character.GetComponent<Character>().Xp += _xpAmount * (int)_xpMultiply;
            Character.GetComponent<Character>().XpSystem();
        }
    }
}

