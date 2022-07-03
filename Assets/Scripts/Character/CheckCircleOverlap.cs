using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scripts
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        //[SerializeField] private string _tag;
        private readonly Collider2D[] _interactionResult = new Collider2D[5]; // Массив с 5 элементами

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
            Handles.color = Color.blue;
           Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius); // Центр, направление, радиус
        }
#endif
    }
}

