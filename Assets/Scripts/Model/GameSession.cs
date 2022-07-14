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
