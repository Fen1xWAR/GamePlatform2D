using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Character>();
            if (hero != null)
            {
                hero.ArmHero();
            }
        }
    }
}
