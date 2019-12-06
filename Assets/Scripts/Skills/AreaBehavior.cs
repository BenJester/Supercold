using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaBehavior : MonoBehaviour
{
    public bool active;
    [HideInInspector] public Thing owner;
    [HideInInspector] public float delay;
    [HideInInspector] public int damage;
    float timer;
    public Text countdownText;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
