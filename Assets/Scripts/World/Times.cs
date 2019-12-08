using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Times : MonoBehaviour
{
    public static Times Instance { get; private set; }

    public float bulletTimeScale = 0.1f;
    public float castTimeScale = 0.5f;
    public float timeScaleEnterLerpRate = 0.07f;
    public float timeScaleExitLerpRate = 0.02f;
    

    float targetTimeScale;
    float targetDeltaTime;

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
            SetTimeScale(castTimeScale);
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.timeScale > targetTimeScale ? timeScaleEnterLerpRate : timeScaleExitLerpRate);
        Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, targetDeltaTime, (Time.timeScale > targetTimeScale ? timeScaleEnterLerpRate : timeScaleExitLerpRate));
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
