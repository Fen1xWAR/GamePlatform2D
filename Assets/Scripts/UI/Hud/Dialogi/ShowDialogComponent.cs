﻿using System;
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
        public void Start()
        {    
            Tag = gameObject.tag;
            Character = GameObject.FindWithTag("Player");
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
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}