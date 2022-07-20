using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts
{
    public class OptionsWindow : WindowController
    {
        private Action _closeAction;

        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }
        public void OnShowMainMenu()
        {
            var window = Resources.Load<GameObject>("UI/MainMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }
        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }
}
