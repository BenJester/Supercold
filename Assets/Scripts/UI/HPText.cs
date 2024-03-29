﻿using System.Collections;
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

    public Image lossHPbar;

    public float lossHPAnimDelay;
    public float lossHPAnimDuration;
    float currDelayTimer;
    bool startTimer;

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
        thing.OnLoseHP += UpdateLostHPUI;
    }

    void UpdateLostHPUI(int lossHP)
    {
        currDelayTimer = lossHPAnimDelay;
        startTimer = true;
    }

    void HandleLostHPUI()
    {
        
        if (currDelayTimer < 0f)
        {
            StartCoroutine(LossHPAnim());
            currDelayTimer = lossHPAnimDelay;
            startTimer = false;
        }
        if (startTimer)
            currDelayTimer -= Time.fixedDeltaTime;
    }

    IEnumerator LossHPAnim()
    {
        float timer = 0f;
        //while (timer < lossHPAnimDuration)
        //{
        //    timer += Time.fixedDeltaTime;
        //    lossHPbar.fillAmount = lossHPbar.fillAmount -
        //                            (lossHPbar.fillAmount - (float) thing.hp / thing.maxHp)
        //                            * (timer / lossHPAnimDuration * lossHPAnimDuration);
        //    yield return new WaitForEndOfFrame();
        //}
        while (lossHPbar.fillAmount > (float)thing.hp / thing.maxHp)
        {
            timer += Time.fixedDeltaTime;
            lossHPbar.fillAmount = lossHPbar.fillAmount -
                                    lossHPAnimDuration;
            yield return new WaitForEndOfFrame();
        }
        lossHPbar.fillAmount = (float) thing.hp / thing.maxHp;
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

        HandleLostHPUI();
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
