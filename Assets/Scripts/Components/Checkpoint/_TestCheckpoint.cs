using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class _TestCheckpoint : MonoBehaviour
    {
        private GameSession _gameSession;

        public void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        public void SetCheckpointOne()
        {
            if (_gameSession.Data.CurrentCheckpoint == 0)
            {
                _gameSession.Data.CurrentCheckpoint = 1;
                CheckpointOne();
            }
            else return;
        }
        private void CheckpointOne()
        {
                Debug.Log("This is test message!!!");
                Debug.Log("You reached checkpoint # 1");
                _gameSession.Data.Coins += 100;
            _gameSession.Data.DoubleJump = true;
            _gameSession.Data.CanAttack = true;
            _gameSession.Data.Damage = 1;
        }
        public void SetCheckpointTwo()
        {
            if (_gameSession.Data.CurrentCheckpoint == 1)
            {
                _gameSession.Data.CurrentCheckpoint = 2;
                CheckpointTwo();
            }
            else return;
        }
        private void CheckpointTwo()
        {
                Debug.Log("This is test message!!!");
                Debug.Log("You reached checkpoint #2");
                _gameSession.Data.Coins += 10;
                _gameSession.Data.MaxHp = 22;
        }
        private void OnApplicationQuit() // Нужно сейв сделать короче, чтоб работал адекватно
        {
            
        }
    }
}