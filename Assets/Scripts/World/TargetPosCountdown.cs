using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPosCountdown : MonoBehaviour
{
    public Text countdown;

    void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        if (Player.Instance.thing.targetPos != null)
        {
            transform.position = Player.Instance.thing.targetPos;
            float countdownVal = (Vector3.Distance(Player.Instance.transform.position, Player.Instance.thing.targetPos)
                         / Player.Instance.thing.speed);
            countdown.text = countdownVal < 0.03f ? "" : countdownVal.ToString("F2");
        }

        

    }
}
