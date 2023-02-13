using UnityEditor;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Defs/Ability", fileName = "AbilityDescription")]
    public class AbilityDef : ScriptableObject
    {
        [SerializeField] private AbilityData _data;

        public AbilityData Data => _data;
    }
}