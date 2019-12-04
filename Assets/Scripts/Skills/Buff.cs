using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Buff")]
public abstract class Buff : ScriptableObject
{
    public string buffName = "New SKill";
    public string detail = "This is a card";
    public float duration = -1f;
    public Thing owner;
    public abstract void Do();
}
