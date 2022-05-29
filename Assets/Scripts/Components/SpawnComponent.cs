using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            //Instantiate(_prefab, _target); // 1- то, что клонировать, 2-“аргет в которой будем создавать префаб
            var instance = Instantiate(_prefab, _target.position, Quaternion.identity); // 1- то, что клонировать, 2- позици€ в мире в которой будем создавать префаб, 3 - отвечает за поворот
            instance.transform.localScale = _target.lossyScale; // Scale мен€етс€ на тот, который у компонента
        }
    }
}
