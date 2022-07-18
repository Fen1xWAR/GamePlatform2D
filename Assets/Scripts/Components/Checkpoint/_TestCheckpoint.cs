using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class _TestCheckpoint : MonoBehaviour
    {
        private Character _character;

        public void Start()
        {
            _character = FindObjectOfType<Character>();
        }
        public void SetCheckpointOne()
        {
            if (_character.CurrentCheckpoint == 0)
            {
                _character.CurrentCheckpoint = 1;
                CheckpointOne();
            }
            else return;
        }
        private void CheckpointOne()
        {
            Debug.Log("This is test message!!!");
            Debug.Log("You reached checkpoint # 1");
            _character.Coins += 100;
            _character.DoubleJump = true;
            _character.CanAttack = true;
            _character.Damage = 1;
        }
        public void SetCheckpointTwo()
        {
            if (_character.CurrentCheckpoint == 1)
            {
                _character.CurrentCheckpoint = 2;
                CheckpointTwo();
            }
            else return;
        }
        private void CheckpointTwo()
        {
            Debug.Log("This is test message!!!");
            Debug.Log("You reached checkpoint #2");
            _character.Coins += 10;
            _character.MaxHp = 22;
        }
        private void OnApplicationQuit() // Нужно сейв сделать короче, чтоб работал адекватно
        {

        }
    }
}