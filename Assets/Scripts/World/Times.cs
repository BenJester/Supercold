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
        Time.timeScale = Mathf.Clamp(Time.timeScale + ((targetTimeScale >= Time.timeScale) ? timeScaleLerpRate : -timeScaleLerpRate), 0.01f, 1f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime + ((targetDeltaTime >= Time.fixedDeltaTime) ? timeScaleLerpRate * startDeltaTime : -timeScaleLerpRate * startDeltaTime), 0.01f * startDeltaTime, startDeltaTime);
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
