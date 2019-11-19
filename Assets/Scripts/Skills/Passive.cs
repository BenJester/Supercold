using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive")]
public abstract class Passive : ScriptableObject
{
    public abstract void Init(GameObject obj);

}
