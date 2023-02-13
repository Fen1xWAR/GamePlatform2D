using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts
{
    public class SkillContent : MonoBehaviour
    {
        private int ID;
        private GameObject _char;
        private void Start()
        {
            _char = GameObject.FindWithTag("Player");
        }
        public void Content()
        {
            switch (ID)
            {
                case 0:
                    Debug.Log("0");
                    _char.GetComponent<Character>().BaseDamage++;
                    break;
                case 1:
                    Debug.Log("1");
                    break;
                case 2:
                    Debug.Log("2");
                    break;
                case 3:
                    Debug.Log("3");
                    break;
                default:
                    Debug.Log("Default");
                    break;
            }
        }
    }
}
