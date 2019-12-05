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
}
