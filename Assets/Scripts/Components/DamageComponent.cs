using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _DamageValue;

        public void ApplyDamage(GameObject target) // Метод, который принимает в себя объект с которым мы столкнулись
        {
            var healthComponent = target.GetComponent<HealthComponent>(); // вызываем компонент HealthComponent с таргета
            if (healthComponent != null) // Проверяем есть ли у объекта компонент healthComponent, если есть то
            {
                healthComponent?.ApllyDamage(_DamageValue); // принимается какой-то урон
            }
        }
    }
}

