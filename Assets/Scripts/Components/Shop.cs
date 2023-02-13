using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class Shop : MonoBehaviour
    {
        private FastTeleport _fastTeleport;

        private GameObject _character;

        private int money;

        public static int FastTeleportPrice = 150;

        private void Start()
        {
            _fastTeleport = FindObjectOfType<FastTeleport>();
            _character = GameObject.FindGameObjectWithTag("Player");
            money = _character.GetComponent<Character>().Coins;
        }

        public void OpenShop()
        {
            _character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
        }

        public void OnCloseShop()
        {
            _character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            SceneManager.UnloadSceneAsync("ShopMenu");
            Cursor.visible = false;
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