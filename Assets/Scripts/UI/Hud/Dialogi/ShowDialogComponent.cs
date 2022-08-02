using System;
using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] public DialogDef external;
        private DialogController _dialogController;

        [SerializeField] public DialogDef[] externalData;
        [SerializeField] private string Tag;
        [SerializeField] private int Checkpoint;
        private GameObject Character;
        private Character _character;
        private Shop _shop;

        [Header("DialogMisc")]
        [SerializeField] private bool ShowFastTeleport;

        private bool FastTeleport;
        private int Money;
        public void Start()
        {    
            Tag = gameObject.tag;
            Character = GameObject.FindWithTag("Player");
            _character = Character.GetComponent<Character>();
            _shop = FindObjectOfType<Shop>();
        }
        public void Show()
        {
            DialogDataCenter();
            if (_dialogController == null)
                _dialogController = FindObjectOfType<DialogController>();
            _dialogController.ShowDialog(Data);
        }

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                        case Mode.External:
                        return external.Data;
                        default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }
        public void DialogDataCenter()
        {
            Checkpoint = Character.GetComponent<Character>().CurrentCheckpoint;
            FastTeleport = Character.GetComponent<Character>().CanFastTeleport;
            Money = Character.GetComponent<Character>().Coins;
            if (Tag == "Dummy")
            {
                if (Checkpoint == 0)
                {
                    external = externalData[0];
                }
                if (Checkpoint == 1)
                {
                    if (external == null || external == externalData[0])
                    {
                        external = externalData[1];
                    }  
                    else if (external == externalData[1])
                    {
                        external = externalData[2];
                    }
                    else
                    {
                        external = externalData[3];
                    }
                }
                else if (Checkpoint == 2)
                {
                    external = externalData[2];
                }
            }else if (Tag == "SignTorgash")
            {
                if (FastTeleport == false && Money < _shop.FastTeleportPrice)
                {
                    external = externalData[2];
                }
                else if (FastTeleport == false)
                {
                    external = externalData[0];

                }else if (FastTeleport == true)
                {
                    external = externalData[1];
                }
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}