﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Skill> skillList;
    public float attackInteval;
    Thing thing;
    bool init;
    public bool ready;

    void Start()
    {
        thing = GetComponent<Thing>(); 
    }



    void DoSkill(Skill skill)
    {

        if (skill.GetType().IsSubclassOf(typeof(ActivateSkill)))
        {
            Action action = new Action(thing, (ActivateSkill) skill);
            action.CastTimeDo();
        }
        else if (skill.GetType().IsSubclassOf(typeof(LockOnSkill)))
        {
            Action action = new Action(thing, (LockOnSkill) skill, Player.Instance.gameObject);
            action.CastTimeDo();
        }
        else if (skill.GetType().IsSubclassOf(typeof(DirectionSkill)))
        {
            Action action = new Action(thing, (DirectionSkill) skill, PredictPosition(Player.Instance.thing, skill));
            action.CastTimeDo();
        }
        ready = true;
    }

    Vector3 PredictPosition(Thing thing, Skill skill)
    {
        return thing.transform.position + Random.Range(0.8f, 1.2f) * thing.speed * (thing.canMove ? 1f : 0.1f) *
            ((Vector3)thing.targetPos - thing.transform.position).normalized 
            * (skill.preCastTime + skill.postCastTime);
    }

    IEnumerator Attack()
    {
        while (!thing.dead && !Player.Instance.thing.dead && skillList.Count > 0)
        {
            foreach (Skill skill in skillList)
            {
                while (skill.range != 0 && Vector3.Distance(transform.position, Player.Instance.transform.position) > skill.range)
                {
                    thing.targetPos = Player.Instance.transform.position;
                    yield return new WaitForEndOfFrame();
                }
                DoSkill(skill);
                
                yield return new WaitForSeconds(attackInteval);
            }
        }
    }

    void Update()
    {
        if (!init)
        {
            init = true;
            Utility.Instance.InitDeck(ref skillList, thing);
            StartCoroutine(Attack());
        }
        
    }
}
