using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Skill : MonoBehaviour
    {
        public string[] SkillName;
        public Sprite SkillSprite;

        [TextArea(1, 3)]
        public string[] SkillDes;
        public Skill[] Parents;
        public int Cost;
        public int Level;
        public int MaxLevel;
        public bool Activated;
        private SkillContent _skillContent;
        private Text LevelText;
        private void Awake()
        {
            _skillContent = GetComponent<SkillContent>();
            LevelText = GetComponentInChildren<Text>();
        }
        public enum State
        {
            Closed,
            NotEnouthPointsToUpgrade,
            Upgradable,
            Upgraded
        }


        public State SkillState;
        public void LevelDisplay()
        {
            LevelText.text = Level.ToString() + "/" + MaxLevel;
        }
        public void OnActivate()
        {
            if (Activated == false)
            {

                _skillContent.Content();

                Cost = Cost * 2;
                if (Level == MaxLevel)
                {
                    Activated = true;
                    SkillState = State.Upgraded;
                }
            }
        }
    }
}
