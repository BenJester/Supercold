using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaknessType
{
    Unknown,
    None,
    ForceWeakness,
    Fire,
    Ice,
    Lightning,
    Sword,
    Rod,
    Axe,
}
public class Weakness
{
    public WeaknessType Type;
    public Sprite sprite;
}
