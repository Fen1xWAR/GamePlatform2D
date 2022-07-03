using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CoinValue : MonoBehaviour
    {
        [SerializeField] private int _coinPrice;
        private void Start()
        {
            
        }
        public void SetCoins(int coins)
        {
            coins += _coinPrice;
            Debug.Log(coins);
            var Coins = GetComponent<Character>();
            
        }
    }

}
