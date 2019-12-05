using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Thing thing;
    public Text text;

    private void Update()
    {
        text.text = thing.hp + " + " + thing.shield;
    }
}
