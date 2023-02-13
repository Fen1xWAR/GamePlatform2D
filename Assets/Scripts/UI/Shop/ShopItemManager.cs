using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ShopItemManager : MonoBehaviour
    {
        [SerializeField] private Text _itemPrice;
        [SerializeField] private Text _itemDescription;
        [SerializeField] private Text _itemName;
        [SerializeField] private int _itemNumber;

        [SerializeField] private Text _buttonOk;
        [SerializeField] private Text _buttonCancel;

        private Shop _shop;
        private Animator _animator;

        public static readonly int FirstItemClosed = Animator.StringToHash("first-closed");

        private void OnEnable()
        {
            // if locale Ru
            _buttonOk.text = "Купить";
            _buttonCancel.text = "Отмена";
            if (_itemNumber == 1)
            {
                gameObject.SetActive(true);
                FirstItemOpen();
            }
        }
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _shop = FindObjectOfType<Shop>();
        }

        public void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }

        public void FirstItemOpen()
        {
            _itemPrice.text = Shop.FastTeleportPrice.ToString();

            //if locale Ru
            _itemName.text = "Альтофчетыре";
            _itemDescription.text = "Этот предмет позволяет облодателю пользоваться магией порталов. Кто знает куда он вас сможет закинуть, может на какой-нибудь остров с викингами?";

            //if locale En
            //_itemName.text = "Altoffour";
            //_itemDescription.text = " ";
        }


        public void FirstItemClose()
        {
            _animator.SetTrigger(FirstItemClosed);
        }
    }
}

