using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class Shop : MonoBehaviour
    {
        private FastTeleport _fastTeleport;

        private GameObject _character;

        private int money;

        [SerializeField] public int FastTeleportPrice = 150;

        private void Start()
        {
            _fastTeleport = FindObjectOfType<FastTeleport>();
            _character = GameObject.FindGameObjectWithTag("Player");
            money = _character.GetComponent<Character>().Coins;
        }

        public void FastTeleport()
        {
            money = _character.GetComponent<Character>().Coins;
            if (money >= FastTeleportPrice)
            {
                _character.GetComponent<Character>().Coins = _character.GetComponent<Character>().Coins - FastTeleportPrice;
                _fastTeleport.GetFastTeleport();
                _character.GetComponent<Character>().SavePlayer();
            } 
            else return;
        }
    }
}