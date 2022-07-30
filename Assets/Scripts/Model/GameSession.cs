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
                DontDestroyOnLoad(this); // ������� ��������� ����� �������
            }
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions) //������� ��������
            {
                if(gameSession != this) // ���� ���� �����-�� ������, ������� �� ����� ������� ������, �� ���������� true
                    return true;
            }

            return false;
        }
    }
}
