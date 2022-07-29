using UnityEditor;
using UnityEngine;
using System;

namespace Scripts
{
    [Serializable]
    public class DialogData
    {
        [SerializeField] private string[] _sentences;
        public string[] Sentences => _sentences;
    }
}