using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class OptionsWindow : WindowController
    {
        private Action _closeAction;

        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;
        private GameObject Character;
        private HudController HudController;

        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
            Character = GameObject.FindWithTag("Player");
            HudController = FindObjectOfType<HudController>();
        }
        public void OnShowMainMenu()
        {
            var window = Resources.Load<GameObject>("UI/MainMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void CloseInHUD()
        {
            HudController.SettingsMenu.active = false;
            Character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Cursor.visible = false;
        }
        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }
}
