using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class FastTeleport : MonoBehaviour
    {
        private Animator _animator;
        private GameObject _character;

        private static readonly int IsEnabled = Animator.StringToHash("is-enabled");
        private static readonly int IsError = Animator.StringToHash("error");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = GameObject.FindGameObjectWithTag("Player");
        }
        private void Start()
        {
            if (_character.GetComponent<Character>().CanFastTeleport == true)
            {
                _animator.SetBool(IsEnabled, true);
            }else
            {
                _animator.SetBool(IsEnabled, false);
            }
        }

        public void GetFastTeleport()
        {
            if (_character.GetComponent<Character>().CanFastTeleport == false)
            {
                _character.GetComponent<Character>().CanFastTeleport = true;
                _animator.SetBool(IsEnabled, true);
            }
            else return;    
        }

        public void Teleport()
        {
            if (_character.GetComponent<Character>().CanFastTeleport == false)
            {
                _animator.SetTrigger(IsError);
            }
            else if (_character.GetComponent<Character>().CanFastTeleport == true)
            {
                _character.GetComponent<Character>().CanFastTeleport = false;
                _character.GetComponent<Character>().SavePlayer();
                _animator.SetBool(IsEnabled, false);
                SceneManager.LoadScene("Island");       
            }
        }
    }
}

