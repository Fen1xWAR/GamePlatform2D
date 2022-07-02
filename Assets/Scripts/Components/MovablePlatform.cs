using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private float moveRadius; // ÑÄÅËÀÒÜ ÄÂÈÆÅÍÈß ÏÎ ÒÎ×ÊÀÌ POS ÈËÈ ×ÅÐÅÇ localPosition(ÍÅ ÐÀÁÎÒÀÅÒ)
        [SerializeField] private float moveSpeed;

        bool movingRight = true;
        private void Update()
        {
            if (transform.localPosition.x > moveRadius)
            {
                movingRight = false;
            }
            else if (transform.localPosition.x < -moveRadius)
            {
                movingRight = true;
            }
            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

            }
        }

    }
}

