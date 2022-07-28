using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class EnemyMassive : MonoBehaviour
    {
        [SerializeField] private GameObject[] Enemies;

        public void SetActive()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].SetActive(true);
            }    
        }

        public void SetNeActive()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].SetActive(false);
            }
        }
    }
}