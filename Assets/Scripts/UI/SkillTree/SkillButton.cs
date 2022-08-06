using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Scripts
{
    public class SkillButton : MonoBehaviour
    {
        public Image SkillImage;
        public Text SkillNameText;
        public Text SkillDescriptionText;

        public int SkillButtonId;

        public void PressSkillButton()
        {
            SkillManager.instance.activateSkill = transform.GetComponent<Skill>();

            SkillImage.sprite = SkillManager.instance.skills[SkillButtonId].SkillSprite;
            SkillNameText.text = SkillManager.instance.skills[SkillButtonId].SkillName;
            SkillDescriptionText.text = SkillManager.instance.skills[SkillButtonId].SkillDes;
        }

    }
}
