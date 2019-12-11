using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs Instance { get; private set; }

    public GameObject bullet;
    public GameObject HPCanvas;
    public GameObject BuffCanvas;
    public GameObject area;
    public GameObject intentionUI;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
