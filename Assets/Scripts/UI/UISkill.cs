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
    public Thing thing;
    public string processedDetail;

    public void ProcessString(string str)
    {
        string res;
        res = str.Replace("dmg", (skill.damage + thing.strength).ToString());
    }

    public void OnSkillClick()
    {
        skill.OnKey();
    }

}
