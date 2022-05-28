using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destinatonTransform;

        public void Teleport(GameObject target) // target - объект который нужно переместить
        {
            target.transform.position = _destinatonTransform.position;
        }
    }
}

