using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CoinValue : MonoBehaviour
    {
        [SerializeField] private GameObject _coinType;
        [SerializeField] private int _coinPrice;
        private int _coins;

        private Character _char;
        private void Awake()
        {
            _char = GetComponent<Character>();
        }
        private void CoinPrice(int Coins)
        {
            _coins += _coinPrice;
            Debug.Log($"{_coinPrice} coins added. Total coins: {_coins}");
        }
    }

}
