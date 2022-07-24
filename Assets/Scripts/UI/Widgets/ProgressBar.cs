using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
} 