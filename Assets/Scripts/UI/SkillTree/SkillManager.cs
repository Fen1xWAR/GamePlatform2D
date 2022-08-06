using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Scripts
{
    public class SkillManager : MonoBehaviour
    {
        public Text PointsText;
        private int remainPoints;
        public int totalPoints;
        public static SkillManager instance;
        public Skill[] skills;
        public SkillButton[] skillButtons;
        public Skill activateSkill;
        [SerializeField] private GameObject UpgradeButton;

        private void Start()
        {
            remainPoints = totalPoints;
            DisplayPoints();
            UpdateSkillButton();

        }
        public void DisplayPoints()
        {
            PointsText.text = remainPoints + "/" + totalPoints;
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
        public void PressUpgrade()
        {
            if (activateSkill.SkillState == Skill.State.Upgradable)
            {
                remainPoints -= activateSkill.Cost;
                activateSkill.Level++;
                activateSkill.OnActivate();
                DisplayPoints();
            }

            UpdateSkillButton();
        }
        void UpdateSkillButton()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i].SkillState == Skill.State.Upgraded)
                {
                    skills[i].isUpgrade = true;
                    skills[i].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                    skills[i].transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);

                }

                else
                {
                    skills[i].transform.GetChild(0).GetComponent<Image>().color = new Vector4(0.25f, 0.25f, 0.25f, 1);
                    var ParentsCount = skills[i].Parents.Length;
                    for (int j = 0; j < ParentsCount; j++)
                    {
                        var activeParents = 0;
                        if (skills[i].Parents[j].isUpgrade)
                        {
                            activeParents++;
                        }
                        if (activeParents == ParentsCount)
                        {
                            skills[i].GetComponent<Button>().interactable = true;
                            if (remainPoints >= skills[i].Cost)
                            {
                                skills[i].SkillState = Skill.State.Upgradable;
                                skills[i].GetComponent<Image>().color = new Vector4(0.15f, 0.45f, 0.45f, 1);

                            }
                            else
                            {
                                skills[i].SkillState = Skill.State.NotEnouthPointsToUpgrade;
                                skills[i].GetComponent<Image>().color = new Vector4(0.5f, 0.15f, 0.15f, 1);
                            }
                        }
                        else
                        {
                            skills[i].SkillState = Skill.State.Closed;
                            skills[i].GetComponent<Image>().color = new Vector4(0.1f, 0.1f, 0.1f, 1);
                        }


                    }
                }
            }
        }
    }
}
