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
        for (int i = 0; i < thing.buffList.Count; i++)
        {
            buffTextList[i].text = thing.buffList[i].buffName + " " + thing.buffList[i].UINum();
        }
    }
}
