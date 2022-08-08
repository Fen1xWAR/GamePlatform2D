using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class OptionsWindow : WindowController
    {
        private Action _closeAction;

        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;
        [SerializeField] private Dropdown ResolutionDropdown;
        private GameObject Character;
        private HudController HudController;
        private Resolution[] resolutions;

        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
            Character = GameObject.FindWithTag("Player");
            HudController = FindObjectOfType<HudController>();
            if (gameObject.CompareTag("HudSettings")) return;
            else
            {
                resolutions = Screen.resolutions;
                ResolutionDropdown.ClearOptions();
                List<string> options = new List<string>();
                int currentResolutionIndex = 0;
                for (int i = 0; i < resolutions.Length; i++)
                {
                    string option = resolutions[i].width + "x" + resolutions[i].height + ":" + resolutions[i].refreshRate + "Hz";
                    options.Add(option);
                    if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = i;
                    }
                }
                ResolutionDropdown.AddOptions(options);
                ResolutionDropdown.value = currentResolutionIndex;
                ResolutionDropdown.RefreshShownValue();
            }
         }

        public void OnShowMainMenu()
        {
            var window = Resources.Load<GameObject>("UI/MainMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void CloseInHUD()
        {
            HudController.SettingsMenu.SetActive(false);
            Character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Cursor.visible = false;
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
        public void SetFullScreen (bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;

        }
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];


            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);


        }
    }
}
