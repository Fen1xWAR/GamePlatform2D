using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBar _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHp;

        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHp = _hp.Health;
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float)hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}