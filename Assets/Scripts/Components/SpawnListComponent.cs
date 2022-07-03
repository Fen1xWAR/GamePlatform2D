using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Scripts
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
            var spawner = _spawners.FirstOrDefault(element => element.Id == id); // Типо foreach перебор элементов массива
            spawner?.Component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component;
        }
    }
}

