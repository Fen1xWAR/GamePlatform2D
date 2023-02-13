using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

namespace Scripts
{
    [Serializable]
    public class DialogData
    {
        [SerializeField]  private string[] _sentences;
        public string[] Sentences => _sentences;

        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private UnityEvent _action;

        public UnityEvent Action => _action;

        public void OpenShop()
        {
            SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
        }
    }
}