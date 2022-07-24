using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class ConfirmNewGameWindow : WindowController
    {
        private Action _closeAction;
        public void Ok()
        {
                string path = Application.persistentDataPath + "/player.nya";
                File.Delete(path);

            _closeAction = () => { SceneManager.LoadScene("Island"); }; // Замыкание, тут на новую игру нужно будет написать какой-нибудь скрипт адекватный
            Close();
        }

        public void Deny()
        {
            var window = Resources.Load<GameObject>("UI/MainMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }
}