using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformPatrolParent : MonoBehaviour
{
    public abstract IEnumerator DoPlatformPatrol();
}
