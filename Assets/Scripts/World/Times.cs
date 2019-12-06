using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Times : MonoBehaviour
{
    public static Times Instance { get; private set; }

    public float bulletTimeScale = 0.1f;

    public float targetTimeScale;
    public float timeScaleLerpRate = 0.07f;
    public float targetDeltaTime;
    public float startDeltaTime;
    public float startTimeScale;

    public float multiplier;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        startDeltaTime = Time.fixedDeltaTime;
        startTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (!Player.Instance.thing.canCast && !Player.Instance.thing.canMove)
            ExitBulletTime();
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, timeScaleLerpRate);
        Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, targetDeltaTime, timeScaleLerpRate * startDeltaTime);
    }

    public void SetTimeScale(float timeScale)
    {
        targetTimeScale = timeScale;
        targetDeltaTime = timeScale * startDeltaTime;
    }

    public void EnterBulletTime()
    {
        targetTimeScale = bulletTimeScale;
        targetDeltaTime = bulletTimeScale * startDeltaTime;
    }

    public void ExitBulletTime()
    {
        targetTimeScale = startTimeScale;
        targetDeltaTime = startDeltaTime;
    }
}
