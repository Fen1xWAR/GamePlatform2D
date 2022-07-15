using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RoomVariants : MonoBehaviour
    {
        [SerializeField] public GameObject[] topLeftRooms;
        [SerializeField] public GameObject[] topMidRooms;
        [SerializeField] public GameObject[] topRightRooms;
        [SerializeField] public GameObject[] bottomLeftRooms;
        [SerializeField] public GameObject[] bottomMidRooms;
        [SerializeField] public GameObject[] bottomRightRooms;
        [SerializeField] public GameObject[] rightTopRooms;
        [SerializeField] public GameObject[] rightMidRooms;
        [SerializeField] public GameObject[] rightDownRooms;
        [SerializeField] public GameObject[] leftTopRooms;
        [SerializeField] public GameObject[] leftMidRooms;
        [SerializeField] public GameObject[] leftDownRooms;
    }
}

