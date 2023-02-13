using UnityEditor;
using UnityEngine;
using System;

namespace Scripts
{
    [Serializable]
    public class AbilityData
    {
        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private string _description;
        public string Description => _description;
    }
}