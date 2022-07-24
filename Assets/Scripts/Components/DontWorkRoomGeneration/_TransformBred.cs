using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

namespace Scripts
{
    public class _TransformBred : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cameraObject;

        public void OnTriggerEnter2D (Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            _cameraObject.Follow = other.transform;
        }
    }
}

