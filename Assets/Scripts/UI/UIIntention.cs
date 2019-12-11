using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntention : MonoBehaviour
{
    [HideInInspector]
    public EnemyAI enemy;
    [HideInInspector]
    public UISkill skillUI;


    private void Start()
    {
        HideIntention();
    }

    public void DisplayIntention()
    {
        skillUI.gameObject.SetActive(true);
        skillUI.DisplaySkill(enemy.GetNextSkill());
    }

    public void HideIntention()
    {
        skillUI.gameObject.SetActive(false);
    }
}
