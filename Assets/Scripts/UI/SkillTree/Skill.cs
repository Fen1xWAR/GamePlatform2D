using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public string SkillName;
    public Sprite SkillSprite;

    [TextArea(1, 3)]
    public string SkillDes;
    public Skill[] Parents;
    public bool isUpgrade;
    public int Cost;
    public int Level;
    public int MaxLevel;
    public bool Activated;

    public enum State
    {
        Closed = 0,
        NotEnouthPointsToUpgrade = 1,
        Upgradable = 2,
        Upgraded = 3
    }
    public State SkillState;

    public void OnActivate()
    {
       if (Activated == false )
        {
            Debug.Log(SkillName + ": Я сработаль! На уровне: " + Level );
            Cost = Cost * 2;
            if (Level == MaxLevel)
            {
                isUpgrade = true;
                Activated = true;
                SkillState = State.Upgraded;
            }
        }
    }
}
