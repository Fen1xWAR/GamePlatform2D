using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class HealingComponent : MonoBehaviour
    {
        [SerializeField] private int _healCount;

        public void ApllyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null) // Проверяем есть ли у объекта компонент healthComponent, если есть то
            {
                healthComponent?.ModifyHealth(_healCount);
            }
        }
    }
}

