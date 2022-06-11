using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class AddCoin : MonoBehaviour
    {
        [SerializeField] private int _numCoins;
        private Character _char;

        private void Start()
        {
            _char = GetComponent<Character>();
        }

        public void Add()
        {
            _char.AddCoins(_numCoins);
        }
    }
}

