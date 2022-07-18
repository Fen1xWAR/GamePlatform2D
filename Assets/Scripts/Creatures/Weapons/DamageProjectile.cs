using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class DamageProjectile : MonoBehaviour
    {
        private int _damage;

        private Character _character;
        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                _damage = _character.ThrowDamage;
                healthComponent.ModifyHealth(_damage);
            }
        }
    }
}

