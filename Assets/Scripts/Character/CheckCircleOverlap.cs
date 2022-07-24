using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;
using System.Linq;

namespace Scripts
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [Header("Radius")]
        [SerializeField] private float _radius = 1f;
        [Header("Method One")]
        [SerializeField] private UnityEvent<GameObject> _action;
        private readonly Collider2D[] _interactionResult = new Collider2D[5]; // Массив с 5 элементами
        [Header("Method Two")]
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverlap;
        public GameObject[] GetObjectsInRange() // Вызов метода
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult); // спавнит вокруг метода круг, заданного радиуса

            var overlaps = new List<GameObject>(); // Создание массивов GameObject'ов, которые вошли в этот круг
            for (var i = 0; i < size; i++)
            {
                overlaps.Add(_interactionResult[i].gameObject);
            }

            return overlaps.ToArray(); // Далее эти объекты уйдут в нужный компонент, в нашем случае он уйдет в Character
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult,
                _mask);

            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var isInTags = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (isInTags)
                {
                    _onOverlap?.Invoke(overlapResult.gameObject);
                }
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
        }
    }
}

