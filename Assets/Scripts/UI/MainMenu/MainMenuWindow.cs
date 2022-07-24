using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class MainMenuWindow : WindowController
    {
        private Action _closeAction;

        public void OnNewGame()
        {
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                var window = Resources.Load<GameObject>("UI/NewGameConfirm");
                var canvas = FindObjectOfType<Canvas>();
                Instantiate(window, canvas.transform);
            }
            else
            {
                _closeAction = () => { SceneManager.LoadScene("Island"); }; // Замыкание, тут на новую игру нужно будет написать какой-нибудь скрипт адекватный
                Close();
            }       
        }

        public void OnLoadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                _closeAction = () => { SceneManager.LoadScene("Island"); };
                Close();
            }
            else
            {
                PlayerData data = SaveSystem.LoadPlayer();
            }           
        }

        public void OnShowOptions()
        {
            var window = Resources.Load<GameObject>("UI/OptionsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void Delete()
        {
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                string path = Application.persistentDataPath + "/player.nya";
                File.Delete(path);
            }
        }

        public void OnExitGame()
        {
            _closeAction = () => 
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }; // Замыкание
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete(); 
        }
    }
}