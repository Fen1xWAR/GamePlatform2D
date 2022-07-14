using UnityEngine;

namespace Scripts
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {     
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}