﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
    ;
public class UISkill : MonoBehaviour
{
    public Text skillName;
    public Text detail;
    public Text castTime;
    [HideInInspector]
    public Skill skill;
    UIIndicator indicator;

    private void Start()
    {
        indicator = Player.Instance.indicator;
    }

    public void DisplaySkill(Skill skill)
    {
        this.skill = skill;
        skillName.text = skill.skillName;
        detail.text = ProcessString(skill, skill.detail);
        castTime.text = (skill.preCastTime + skill.postCastTime).ToString("F2") + "s";
    }

    public void OnSkillClick()
    {
        if (skill.owner != Player.Instance.thing) return;
        skill.OnKey();
    }

    public string ProcessString(Skill skill, string str)
    {
        string res;
        res = str.Replace("dmg", (skill.damage + skill.owner.strength).ToString());
        return res;
    }

    public void ShowCircle()
    {
        indicator.ShowCircle(skill);
    }

    public void HideCircle()
    {
        indicator.HideCircle();
    }
}
