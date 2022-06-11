using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _payerData;

        public PlayerData Data  => _payerData;

        private void Awake()
        {
            if(IsSessionExist())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this); // ������� ��������� ����� �������
            }
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
