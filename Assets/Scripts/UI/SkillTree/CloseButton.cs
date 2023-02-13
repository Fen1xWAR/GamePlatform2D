using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class CloseButton : MonoBehaviour
    {
        private GameObject SkillTree;
        private GameObject _char;
        private void Awake()
        {
            SkillTree = GameObject.FindWithTag("SkillTree");
            _char = GameObject.FindGameObjectWithTag("Player");
            
        }
        public void Close()
        {
            _char.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            SkillTree.active = false;
            Cursor.visible = false;
        }
    }
}