using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class EzUI : MonoBehaviour
    {
        private GameObject _character;
        private DialogController _dialogController;
        private DialogDef _dialogDef;

        private void Start()
        {
            _character = GameObject.FindGameObjectWithTag("Player");
            _dialogController = FindObjectOfType<DialogController>();
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
            _character.GetComponent<Character>().isShopOpened = false;
            if (_dialogController != null)
            {
                if (_dialogController._typingRoutine != null)
                {
                    _dialogController.StopCoroutine(_dialogController._typingRoutine);
                    _dialogController._typingRoutine = null;
                    _dialogController?.HideDialohBox();
                }
            }
        }
    }
}