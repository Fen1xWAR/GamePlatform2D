using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public abstract class Patrol : MonoBehaviour // abstract - ������� ����� �� �����, �� ����� ���������� ����������
    {
        public abstract IEnumerator DoPatrol();
    }

}