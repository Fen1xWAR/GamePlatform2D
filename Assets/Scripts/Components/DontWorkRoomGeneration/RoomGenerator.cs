/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        public enum Direction
        {
            Top,
            Bottom,
            Left,
            Right,
            None
        }

        private RoomVariants variants;
        private int _rand;
        private bool _spawned = false;
        private float _waitTime = 3f;

        private void Awake()
        {
            variants = GetComponent<RoomVariants>();
        }
        private void Start()
        {
            variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
            Destroy(gameObject, _waitTime);
            Invoke("Spawn", 0.2f);
        }

        public void Spawn()
        {
            if (!_spawned)
            {
                if (_direction == Direction.Top)
                {
                    _rand = Random.Range(0, variants.topRooms.Length);
                    Instantiate(variants.topRooms[_rand], transform.position, variants.topRooms[_rand].transform.rotation);
                }else if (_direction == Direction.Bottom)
                {
                    _rand = Random.Range(0, variants.bottomRooms.Length);
                    Instantiate(variants.bottomRooms[_rand], transform.position, variants.bottomRooms[_rand].transform.rotation);
                }
                else if (_direction == Direction.Left)
                {
                    _rand = Random.Range(0, variants.leftRooms.Length);
                    Instantiate(variants.leftRooms[_rand], transform.position, variants.leftRooms[_rand].transform.rotation);
                }
                else if (_direction == Direction.Right)
                {
                    _rand = Random.Range(0, variants.rightRooms.Length);
                    Instantiate(variants.rightRooms[_rand], transform.position, variants.rightRooms[_rand].transform.rotation);
                }
                _spawned = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag("RoomPoint") && other.GetComponent<RoomGenerator>()._spawned)
            {
                Destroy(gameObject);
            }
        }
    }
}
*/