using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private Collider2D _Collider;

    public bool IsTouchingLayer;

    private void Awake()
    {
        _Collider = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTouchingLayer = _Collider.IsTouchingLayers(_groundLayer);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingLayer = _Collider.IsTouchingLayers(_groundLayer);
    }
}
