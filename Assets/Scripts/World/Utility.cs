using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Utility Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public LayerMask thingLayer;
    public LayerMask bulletLayer;
    public Sprite fire;
    public Sprite ice;
    public Sprite lightning;
    public Sprite sword;
    public Sprite rod;
    public Sprite axe;
    public Sprite None;
    public Sprite ForceWeakness;
    public Sprite Unknown;
    public Buff breakBuff;

    public void InitDeck(ref List<Skill> deck, Thing owner)
    {
        List<Skill> res = new List<Skill>();
        foreach (var skill in deck)
        {
            Skill newSkill = Instantiate(skill);
            newSkill.owner = owner;
            res.Add(newSkill);

        }
        deck = res;
    }

    public Sprite GetWeaknessSprite(WeaknessType weaknessType)
    {
        switch (weaknessType)
        {
            case WeaknessType.Unknown:
                return Unknown;
            case WeaknessType.None:
                return None;
            case WeaknessType.ForceWeakness:
                return ForceWeakness;
            case WeaknessType.Fire:
                return fire;
            case WeaknessType.Ice:
                return ice;
            case WeaknessType.Lightning:
                return lightning;
            case WeaknessType.Sword:
                return sword;
            case WeaknessType.Rod:
                return rod;
            case WeaknessType.Axe:
                return axe;
            default:
                return null;
        }
    }
}
