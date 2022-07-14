using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _payerData;
        public PlayerData Data  => _payerData;
        private PlayerData _save;

        private void Awake()
        {
            if(IsSessionExist())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                DontDestroyOnLoad(this); // создает хранилище между сценами
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions) //Перебор объектов
            {
                if(gameSession != this) // Если есть какая-то сессия, которая не равна текущей сессии, то возвращаем true
                    return true;
            }

            return false;
        }

        public void Save()
        {
            _save = _payerData.Clone();
        }

        public void LoadLastSave()
        {
            _payerData = _save.Clone();
        }
    }
}
