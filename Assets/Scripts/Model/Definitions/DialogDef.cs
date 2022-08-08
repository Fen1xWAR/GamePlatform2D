using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Defs/Dialog", fileName = "Dialog")]
   public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;

        private Shop _shop;
        private GameObject _character;

        public void OpenShop()
        {
                _character = GameObject.FindGameObjectWithTag("Player");
            if (_character.GetComponent<Character>().isShopOpened == true)
            {
                return;
            }
            else if (_character.GetComponent<Character>().isShopOpened == false)
            {
                _character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                Cursor.visible = true;
                SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
                _character.GetComponent<Character>().isShopOpened = true;
            }
        }
    }
}