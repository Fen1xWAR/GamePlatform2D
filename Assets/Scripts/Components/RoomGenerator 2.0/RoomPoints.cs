using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RoomPoints : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        public enum Direction
        {
            TopLeft,
            TopMid,
            TopRight,
            BottompLeft,
            BottomMid,
            BottomRight,
            RightTop,
            RightMid,
            RightDown,
            LeftTop,
            LeftMid,
            LeftDown,
            None
        }
        private RoomVariants _roomVariants;
        private bool _spawned;

        private void Awake()
        {
            _roomVariants = GetComponent<RoomVariants>();
        }

        private void Start()
        {
            _roomVariants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        }
        private void Spawn()
        {
            if (!_spawned)
            {

            }
        }
    }
}

