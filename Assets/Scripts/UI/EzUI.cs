using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class EzUI : MonoBehaviour
    {
        private GameObject _character;

        private void Start()
        {
            _character = GameObject.FindGameObjectWithTag("Player");
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
    }
}