using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class IsShopOpenTrigger : MonoBehaviour
    {
        public Animator _animator;
        public static readonly int IsShopOpen = Animator.StringToHash("is-opened");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(IsShopOpen, true);
        }

        public void Disable()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(IsShopOpen, false);
            SceneManager.UnloadSceneAsync("ShopMenu");
        }
    }
}