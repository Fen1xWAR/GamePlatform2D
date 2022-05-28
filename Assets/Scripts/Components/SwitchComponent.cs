using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;
        public void Switch()
        {
            _state = !_state; // Во время стейта менять состояние
            _animator.SetBool(_animationKey, _state); // и будем ставить нужный bool
        }

        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}
