using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuff : MonoBehaviour
{
    public Text shortDetail;
    public Text detail;

    private void Start()
    {
        detail.enabled = false;
    }

    public void OnMouseOver()
    {
        detail.enabled = true;
    }

    public void OnMouseExit()
    {
        detail.enabled = false;
    }
}
