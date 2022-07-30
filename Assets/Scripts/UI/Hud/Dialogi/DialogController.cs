using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Text _name;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")]
        [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;
        private ShowDialogComponent _showDialog;

        private GameObject _char;

        private static readonly int IsOpen = Animator.StringToHash("is-open");

        private void Start()
        {
            _char = GameObject.FindWithTag("Player");
            Cursor.visible = false;
            _sfxSource = AudioUtils.FindSfxSource();
        }
        public void ShowDialog(DialogData data)
        {
            _char.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Cursor.visible = true;
            _data = data; // Сохраним данные
            _currentSentence = 0;
            _text.text = string.Empty;
            _name.text = _data.Name;
            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }

        private IEnumerator TypeDialogText()
        {
            _text.text = String.Empty;
            var sentence = _data.Sentences[_currentSentence];
            foreach (var letter in sentence)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }
            _typingRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }

        public void StopTypeAnimation()
        {
            if (_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
                _typingRoutine = null;
            } 
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogComplete = _currentSentence >= _data.Sentences.Length;
            if (isDialogComplete)
            {
                HideDialohBox();
            }
            else
            {
                OnStartDialog();
            }
        }

        private void HideDialohBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
            _char.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Cursor.visible = false;
        }

        public void OnStartDialog()
        {
            _typingRoutine = StartCoroutine(TypeDialogText());
        }

        public void OnCloseDialog()
        {

        }
    }
}