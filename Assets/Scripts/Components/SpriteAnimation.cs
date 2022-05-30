using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))] // ��������� ���� ���������, ���� ��� ���
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int _frameRate; // ���������
    [SerializeField] private bool _loop; // �������������
    [SerializeField] private Sprite[] _sprites; // ������ ��������
    [SerializeField] private UnityEvent _onComplete; // �����, ������� ����� ���������� �� �������� ��������

    private SpriteRenderer _spriteRenderer;
    private float _secondsPerFrame;
    private int _currentSprtie;
    private float _nextFrameTime;

    private bool _isPlaying = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate; // �� ���� �������� � ��� ����� ��� ����������
        _nextFrameTime = Time.time + _secondsPerFrame; // ����� ������ ����������, ������� ����� + ���������� ������ �� �����
    }
    private void Update()
    {
        if (!_isPlaying || _nextFrameTime > Time.time) return; // ���� ��������� ����� ����� ����� ������� ����, ��

        if(_currentSprtie >= _sprites.Length)
        {
            if(_loop)
            {
                _currentSprtie = 0;
            }
            else
            {
                _isPlaying = false;
                _onComplete?.Invoke();
                return;
            }
        }

        _spriteRenderer.sprite = _sprites[_currentSprtie]; // ��������� ������ ��� ����, �� ������� �������
        _nextFrameTime += _secondsPerFrame; // ��������� ����� ���������� ������� �����
        _currentSprtie++; // � �������, ��� ������� ��������� ����
    }
}
