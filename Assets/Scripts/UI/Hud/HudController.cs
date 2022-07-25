using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class HudController : MonoBehaviour
    {
        [Header("Health hud")]
        [SerializeField] private ProgressBar _healthBar;
        [SerializeField] private Text _Hp;
        [SerializeField] private Text _maxHp; 

        [Header("Level hud")]
        [SerializeField] private ProgressBar _xpBar;
        [SerializeField] private Text _xp;
        [SerializeField] private Text _xpToUp;
        [SerializeField] private Text _level;

        [Header("Coins")]
        [SerializeField] private Text _coins;
        private GameObject Character;

        private void Start()
        {
            Character = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            // HP
            var maxHealth = Character.GetComponent<Character>().MaxHp;
            var Hp = Character.GetComponent<Character>().Hp;

            _maxHp.text = maxHealth.ToString();
            _Hp.text = Hp.ToString();

            var hpValue = (float)Hp / maxHealth;
            _healthBar.SetProgress(hpValue);

            //LEVEL
            var level = Character.GetComponent<Character>().Level;
            var xp = Character.GetComponent<Character>().Xp;
            var xpToUp = Character.GetComponent<Character>().XpToUp;

            _level.text = level.ToString();
            _xp.text = xp.ToString();
            _xpToUp.text = xpToUp.ToString();

            var xpValue = (float)xp / xpToUp;
            _xpBar.SetProgress(xpValue);

            //COINS
            var coins = Character.GetComponent<Character>().Coins;
            _coins.text = coins.ToString();
        }
    }
}