using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
    ;
public class UISkill : MonoBehaviour
{
    public Text skillName;
    public Text detail;
    public Text castTime;
    public Image cooldownImg;
    [SerializeField]
    public List<Image> weaknessImgList;
    [HideInInspector]
    public Skill skill;
    UIIndicator indicator;

    private void Start()
    {
        indicator = Player.Instance.indicator;
    }

    public void DisplayCooldown(float curCD, float maxCD)
    {
        if (skill != null && skill.cooldown != 0f)
        {
            if (curCD != 0f)
                castTime.text = curCD.ToString("F2") + "s";
            else
                castTime.text = skill.cooldown.ToString("F2") + "s";
            if (maxCD != 0f)
                cooldownImg.fillAmount = curCD / maxCD;
            else
                cooldownImg.fillAmount = 0;
        }
        
    }

    public void DisplaySkill(Skill skill)
    {
        this.skill = skill;
        skillName.text = skill.skillName;
        detail.text = ProcessString(skill, skill.detail);
        //castTime.text = (skill.preCastTime + skill.postCastTime).ToString("F2") + "s";

        foreach (Image image in weaknessImgList)
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }
        for (int i = 0; i < skill.weaknessList.Count; i++)
        {
            weaknessImgList[0].color = Color.white;
            weaknessImgList[0].sprite = Utility.Instance.GetWeaknessSprite(skill.weaknessList[i]);
        }
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
