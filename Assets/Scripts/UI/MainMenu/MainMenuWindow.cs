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
            _closeAction = () => { SceneManager.LoadScene("Island"); }; // Замыкание
            CloseWindow();
        }

        public void OnLoadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/player.nya"))
            {
                _closeAction = () => { SceneManager.LoadScene("Island"); };
                CloseWindow();
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

        public void OnExitGame()
        {
            _closeAction = () => 
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }; // Замыкание
            CloseWindow();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete(); 
        }
    }
}