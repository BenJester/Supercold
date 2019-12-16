using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffPanel : MonoBehaviour
{
    public List<UIBuff> buffUIList;
    public Thing thing;
    public GameObject buffTextObj;
    public int maxBuffNum;

    void Start()
    {
        
        for (int i = 0; i < maxBuffNum; i++)
        {
            UIBuff uiBuff = Instantiate(buffTextObj, transform).GetComponent<UIBuff>();
            buffUIList.Add(uiBuff);
        }
        
    }

    void Update()
    {
        thing = Player.Instance.thing;
        int j = 0;
        for (int i = 0; i < thing.buffList.Count; i++)
        {
            if (thing.buffList[i].active)
            {
                Buff buff = thing.buffList[i];
                buffUIList[j].shortDetail.text = buff.buffName + " "
                    + buff.UINum() + " "
                    + (buff.currDuration != -1f ? buff.currDuration.ToString("F2") : "∞");
                buffUIList[j].detail.text = ProcessString(buff, buff.detail);
                j += 1;
            }
            else
            {
                buffUIList[j].shortDetail.text = "";
                buffUIList[j].detail.text = "";
            }
                
        }
    }

    public string ProcessString(Buff buff, string str)
    {
        string res;
        res = str.Replace("dur", buff.duration.ToString());
        return res;
    }
}
