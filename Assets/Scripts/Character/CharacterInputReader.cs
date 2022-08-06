using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class CharacterInputReader : MonoBehaviour
    {
        [SerializeField] private Character _char;
        [SerializeField] private DialogController _dialogController;
        private HudController _hudController;
        private FastTeleport _fastTeleport;



        public void Start()
        {
            _dialogController = FindObjectOfType<DialogController>();
            _hudController = FindObjectOfType<HudController>();
            _fastTeleport = FindObjectOfType<FastTeleport>();
        }

        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
           
            var direction = context.ReadValue<Vector2>();
            _char.SetDirection(direction);
        }

 /*       public void OnVerticalMovement(InputAction.CallbackContext context)
        {
                var direction = context.ReadValue<Vector2>();
            if (context.performed)
            {
                _char.SetDirection(direction);
            }                
        }*/

        public void OnDialogSettings(InputAction.CallbackContext context)
        {
            if (_dialogController._container.active)
            {
                if (context.canceled) // Отпустили клавишу
                {
                    _char.SkipDialog();
                }
            }
            else return;
        }

  /*      public void OnOpenMenu(InputAction.CallbackContext context)
        {
            if (context.canceled) // Отпустили клавишу
            {
               _hudController.ShowSettings();
            }  
        }*/ //Need to fix

        public void OnFastTeleport(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _char.UseFastTeleport();
            }   
        }

        public void OnInteract(InputAction.CallbackContext context) // OnInteract - зависит от того как назвали в схеме управления, чтобы он подхватил эту кнопку
        {
            if (context.canceled) // Отпустили клавишу
            {
                _char.Interact();
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.canceled) // Отпустили клавишу
            {
                _char.Attack();
            }
        }

        public void OnThrowAttack(InputAction.CallbackContext context)
        {
            if (context.performed) // Действие совершится, когда мы нажали на кнопку
            {
                _char.ThrowAttack();
            }
        }
        public void OnSkillTreeOpen(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {

                _char.SkillTree();
            }
        }
    }
}


