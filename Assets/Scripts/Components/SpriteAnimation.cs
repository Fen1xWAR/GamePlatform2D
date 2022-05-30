using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))] // Назначить этот компонент, если его нет
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int _frameRate; // Фреимрейт
    [SerializeField] private bool _loop; // Зацикленность
    [SerializeField] private Sprite[] _sprites; // Массив спрайтов
    [SerializeField] private UnityEvent _onComplete; // Ивент, который будет вызываться по окнчанию анимации

    private SpriteRenderer _spriteRenderer;
    private float _secondsPerFrame;
    private int _currentSprtie;
    private float _nextFrameTime;

    private bool _isPlaying = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate; // На одну секундку у нас будет эта переменная
        _nextFrameTime = Time.time + _secondsPerFrame; // Время нашего обновления, текущее время + количество секунд на фрейм
    }
    private void Update()
    {
        if (!_isPlaying || _nextFrameTime > Time.time) return; // Если наступило время когда нужно сменить кадр, то

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

        _spriteRenderer.sprite = _sprites[_currentSprtie]; // Назначаем нужный нам кадр, по дефолту нулевой
        _nextFrameTime += _secondsPerFrame; // Обновляем время следующего апдейта кадра
        _currentSprtie++; // И говорим, что обновим следующий кадр
    }
}
