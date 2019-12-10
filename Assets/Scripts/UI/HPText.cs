using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Thing thing;
    public Text hpText;
    public Text shieldText;
    public Image hpBar;

    public Image castTimeBar;
    public Image castTimeBg;
    public Text castTimeText;

    public Image debuffTimerBar;
    public Image debuffTimeBg;
    public Text debuffTimeText;

    Buff currBuff;

    float currCastTime;
    float totalCastTime;
    bool showCastTime;

    float currDebuffTime;
    float totalDebuffTime;
    bool showDebuffTime;

    private void Start()
    {
        HideCastTimeBar();
        HideDebuffTimeBar();
    }

    private void Update()
    {
        hpText.text = thing.hp + " / " + thing.maxHp;
        shieldText.text = thing.shield.ToString();
        hpBar.fillAmount =  (float) thing.hp / thing.maxHp;

        if (showCastTime)
        {
            castTimeBar.fillAmount = currCastTime / totalCastTime;
            castTimeText.text = (totalCastTime - currCastTime).ToString("F2");
            currCastTime += Time.fixedDeltaTime;
            if (currCastTime >= totalCastTime)
                HideCastTimeBar();
        }

        if (showDebuffTime)
        {
            debuffTimerBar.fillAmount = currDebuffTime / totalDebuffTime;
            debuffTimeText.text = currBuff.buffName + " " + (totalDebuffTime - currDebuffTime).ToString("F2");
            currDebuffTime += Time.fixedDeltaTime;
            if (currDebuffTime >= totalDebuffTime)
                HideCastTimeBar();
        }
    }

    public void ShowCastTimeBar(float castTime)
    {
        castTimeBar.enabled = true;
        castTimeBg.enabled = true;
        totalCastTime = castTime;
        currCastTime = 0f;
        showCastTime = true;
    }

    public void ShowDebuffTimeBar(float debuffTime, Buff buff)
    {
        debuffTimerBar.enabled = true;
        debuffTimeBg.enabled = true;
        totalDebuffTime = debuffTime;
        currDebuffTime = 0f;
        showDebuffTime = true;
        if (currBuff == null || buff.currDuration > currBuff.currDuration)
            currBuff = buff;
    }

    public void HideDebuffTimeBar()
    {
        debuffTimerBar.enabled = false;
        debuffTimeBg.enabled = false;
        showDebuffTime = false;
        debuffTimeText.text = "";
    }

    public void HideCastTimeBar()
    {
        castTimeBar.enabled = false;
        castTimeBg.enabled = false;
        showCastTime = false;
        castTimeText.text = "";
    }
}
