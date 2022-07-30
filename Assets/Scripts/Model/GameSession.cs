using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class GameSession : MonoBehaviour
    {
        private void Awake()
        {
            LoadHud();
            if (IsSessionExist())
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this); // создает хранилище между сценами
            }
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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
    }
}
