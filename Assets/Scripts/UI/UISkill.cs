using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
    ;
public class UISkill : MonoBehaviour
{
    public Text skillName;
    public Text detail;
    public Text CastTime;
    public Skill skill;

    public void ProcessString()
    {

    }

    public void OnSkillClick()
    {
        skill.OnKey();
    }

}
