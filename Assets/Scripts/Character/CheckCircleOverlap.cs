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
        private readonly Collider2D[] _interactionResult = new Collider2D[5]; // ������ � 5 ����������

        public GameObject[] GetObjectsInRange() // ����� ������
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult); // ������� ������ ������ ����, ��������� �������

            var overlaps = new List<GameObject>(); // �������� �������� GameObject'��, ������� ����� � ���� ����
            for (var i = 0; i < size; i++)
            {
                overlaps.Add(_interactionResult[i].gameObject);
            }

            return overlaps.ToArray(); // ����� ��� ������� ����� � ������ ���������, � ����� ������ �� ����� � Character
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.blue;
           Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius); // �����, �����������, ������
        }
#endif
    }
}

