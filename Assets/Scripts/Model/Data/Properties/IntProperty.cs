using System;
using UnityEditor;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    public class IntProperty : PersistentProperty<int>
    {
        public IntProperty(int defaultValue) : base(defaultValue)
        {
        }

        protected override void Write(int value)
        {
            _value = value;
        }
        protected override int Read(int defaultValue)
        {
            return _value;
        }  
    }
}