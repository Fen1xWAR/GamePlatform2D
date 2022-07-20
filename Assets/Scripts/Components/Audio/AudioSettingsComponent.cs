using UnityEditor;
using UnityEngine;
using System;

namespace Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private SoundSetting _mode;
        private FloatPersistentProperty _model;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _model = FindProperty();
            _model.OnChanged += OnSoundSettingsChanged;
            OnSoundSettingsChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingsChanged(float newValue, float oldValue)
        {
            _audioSource.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSetting.Music:
                    return GameSettings.I.Music;
                case SoundSetting.Sfx:
                    return GameSettings.I.Sfx;
            }

            throw new ArgumentException("Undefined mode");
        }
    }
}