using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public abstract class Patrol : MonoBehaviour // abstract - создать метод не можем, но можем определить наследника
    {
        public abstract IEnumerator DoPatrol();
    }

}