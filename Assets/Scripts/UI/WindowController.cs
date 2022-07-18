using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class WindowController : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();

            _animator.SetTrigger(Show);
        }

        public void CloseWindow()
        {
            _animator.SetTrigger("Hide");
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}