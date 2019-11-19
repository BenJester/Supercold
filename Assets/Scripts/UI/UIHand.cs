using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHand : MonoBehaviour
{
    public List<UISkill> skillList;

    private void Update()
    {
        for (int i = 0; i < skillList.Count; i ++)
        {
            skillList[i].skillName.text = Player.Instance.Hand[i].skillName;
            skillList[i].detail.text = Player.Instance.Hand[i].detail;
        }
    }
}
