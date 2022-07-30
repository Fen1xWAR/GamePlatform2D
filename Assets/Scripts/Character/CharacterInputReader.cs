using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class CharacterInputReader : MonoBehaviour
    {
        [SerializeField] private Character _char;

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
    }
}


