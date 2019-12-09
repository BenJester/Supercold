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

    private void Update()
    {
        hpText.text = thing.hp + " / " + thing.maxHp;
        shieldText.text = thing.shield.ToString();
        hpBar.fillAmount =  (float) thing.hp / thing.maxHp;
    }
}
