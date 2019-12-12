using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffPanel : MonoBehaviour
{
    public List<Text> buffTextList;
    public Thing thing;
    public GameObject buffTextObj;
    public int maxBuffNum;

    void Start()
    {
        
        for (int i = 0; i < maxBuffNum; i++)
        {
            Text text = Instantiate(buffTextObj, transform).GetComponent<Text>();
            buffTextList.Add(text);
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
                buffTextList[j].text = thing.buffList[i].buffName + " "
                    + thing.buffList[i].UINum() + " "
                    + (thing.buffList[i].currDuration != -1f ? thing.buffList[i].currDuration.ToString("F2") : "");
                j += 1;
            }
            else
                buffTextList[j].text = "";
        }
    }
}
