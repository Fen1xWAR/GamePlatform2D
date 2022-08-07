using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class CloseButton : MonoBehaviour
    {
        private GameObject _char;
        private void Awake()
        {
            _char = GameObject.FindWithTag("Player");
        }
        public void Close()
        {
            _char.GetComponent<Character>().SkillTree();
        }
    }
}