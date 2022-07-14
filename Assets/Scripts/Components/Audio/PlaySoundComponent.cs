using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts
{
    public class PlaySoundComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;
                
                    _source.PlayOneShot(audioData.Clip);
                    break;              
            }
        }
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id; // Это получается изначально они у нас открытые, но чтобы их нельзя было менять извне, мы их передаем
        public AudioClip Clip => _clip;
    }
}
