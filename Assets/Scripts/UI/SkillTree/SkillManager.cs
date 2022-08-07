using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Scripts
{
    public class SkillManager : MonoBehaviour
    {
        public Text LevelText;
        private int Points;
        public Text PointsText;
        public static SkillManager instance;
        public Skill[] skills;
        public SkillButton[] skillButtons;
        public Skill activateSkill;
        [SerializeField] private GameObject UpgradeButton;
        public int[] SkillsLevel;
        private GameObject _char;
        public Text TextOnUpgradeButton;
        private void Start()
        {



            _char = GameObject.FindWithTag("Player");
          //  Points = _char.GetComponent<Character>().AbilPoint;
         //   SkillsLevel = _char.GetComponent<Character>().SkillsLevels;
            Loader();
            UpdateSkillButton();
            DisplayPoints();

            //Вот тут надо вьебать чтобы из чара брался массив уровней навыков
        }
        public void DisplayPoints()
        {
            PointsText.text = Points.ToString();
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
        private void PressUpgrade()
        {
            if (activateSkill.SkillState == Skill.State.Upgradable)
            {
                Points -= activateSkill.Cost;
                activateSkill.Level++;
                activateSkill.OnActivate();
                DisplayPoints();
                Saver();
                UpdateSkillCostDisplay(activateSkill.GetComponent<SkillButton>().SkillButtonId);
            }

            UpdateSkillButton();
        }
        public void SetUpgraded(int ID)
        {
            skills[ID].GetComponent<Button>().interactable = true;
            skills[ID].SkillState = Skill.State.Upgraded;
            skills[ID].Activated = true;
            skills[ID].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            skills[ID].transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }

        private static bool ParentsCheck(Skill skill)
        {
            var activeParents = 0;
            var ParentsCount = skill.Parents.Length;
            for (int j = 0; j < ParentsCount; j++)
            {

                if (skill.Parents[j].Activated)
                {
                    activeParents++;
                }
                
            }
            if (activeParents == ParentsCount)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public void SetUpgradeble(int ID)
        {
            skills[ID].GetComponent<Button>().interactable = true;
            if (Points >= skills[ID].Cost)
            {
                skills[ID].SkillState = Skill.State.Upgradable;
                skills[ID].GetComponent<Image>().color = new Vector4(0.15f, 0.45f, 0.45f, 1);

            }
            else
            {
                skills[ID].SkillState = Skill.State.NotEnouthPointsToUpgrade;
                skills[ID].GetComponent<Image>().color = new Vector4(0.5f, 0.15f, 0.15f, 1);
            }
        }
        public void Loader() 
        {
            Points = _char.GetComponent<Character>().AbilPoint;
            SkillsLevel = _char.GetComponent<Character>().SkillsLevels ;
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].Level = SkillsLevel[i];

                //    skills[i].Level = _char.GetComponent<Character>().SkillsLevels[i];

            }
        }
        public void Saver()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                SkillsLevel[i] = skills[i].Level;
            }
            _char.GetComponent<Character>().SkillsLevels = SkillsLevel;
            _char.GetComponent<Character>().AbilPoint = Points;
           _char.GetComponent<Character>().SavePlayer();
        }
        public void UpdateSkillButton()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].LevelDisplay();
                if (skills[i].Level == skills[i].MaxLevel)
                {
                    SetUpgraded(i);
                }
                else
                {
                    skills[i].transform.GetChild(0).GetComponent<Image>().color = new Vector4(0.25f, 0.25f, 0.25f, 1);
                    if (ParentsCheck(skills[i])) 
                    {
                        SetUpgradeble(i);
                    }
                    else
                    {
                        skills[i].SkillState = Skill.State.Closed;
                        skills[i].GetComponent<Image>().color = new Vector4(0.1f, 0.1f, 0.1f, 1);
                    }
                  

                }
            }
        }
        public void UpdateSkillCostDisplay(int ID)
        {
            if (skills[ID].SkillState == Skill.State.Upgraded)
            {
                TextOnUpgradeButton.text = "MaxLevelUpgraded!";
                TextOnUpgradeButton.color = new Vector4(0.15f, 0.45f, 0.45f, 1);
            }
            else
            {
                TextOnUpgradeButton.text = "UpgradeCost: " + skills[ID].Cost.ToString();
                if (skills[ID].SkillState == Skill.State.NotEnouthPointsToUpgrade)
                {
                    TextOnUpgradeButton.color = new Vector4(0.5f, 0.15f, 0.15f, 1);
                }
                else
                {
                    TextOnUpgradeButton.color = new Vector4(0.15f, 0.45f, 0.45f, 1);
                }
            }
        }
        private void OnDestroy()
        {
            Saver();   
        }
    }

}

      