using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class FreezeChar : MonoBehaviour
    {
        private GameObject _char;
        private IEnumerator _delay;
        private void Start()
        {
            _char = GameObject.FindGameObjectWithTag("Player");
            _delay = delay();
        }

        private void Update()
        {
            StartCoroutine(_delay);
            _char.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        private IEnumerator delay()
        {
            yield return new WaitForSeconds(1f);
        }

        private void OnDisable()
        {
            _char.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}