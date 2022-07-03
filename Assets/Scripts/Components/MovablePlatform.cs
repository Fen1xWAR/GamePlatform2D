using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MovablePlatform : MonoBehaviour
    {
        //[SerializeField] private float moveRadius; // —ƒ≈À¿“‹ ƒ¬»∆≈Õ»ﬂ œŒ “Œ◊ ¿Ã POS »À» ◊≈–≈« localPosition(Õ≈ –¿¡Œ“¿≈“)
        [SerializeField] private float moveSpeed;
        //[SerializeField] private Transform platform;
        [SerializeField] private Transform pos1;
        [SerializeField] private Transform pos2;
        [SerializeField] private Transform startPos;
        bool movingRight = true;

        Vector3 nextPos;

         private void Start()
         {
             nextPos = startPos.position;
         }

         private void Update()
         {
             transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);

             if (transform.position == pos1.position)
            {
                nextPos = pos2.position;
                movingRight = false;
                transform.position = Vector3.MoveTowards(transform.position, nextPos, -moveSpeed * Time.deltaTime);
            }
            if (transform.position == pos2.position)
            {
                nextPos = pos1.position;
                movingRight = true;
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
            }
        }
    }
}

