using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Skill> skillList;
    public float attackInteval;
    Thing thing;
    bool init;

    void Start()
    {
        thing = GetComponent<Thing>(); 
    }



    void DoSkill(Skill skill)
    {

        if (skill.GetType().IsSubclassOf(typeof(ActivateSkill)))
        {
            Action action = new Action(thing, (ActivateSkill) skill);
            action.Do();
        }
        else if (skill.GetType().IsSubclassOf(typeof(LockOnSkill)))
        {
            Action action = new Action(thing, (LockOnSkill) skill, Player.Instance.gameObject);
            action.Do();
        }
    }

    IEnumerator Attack()
    {
        while (!thing.dead && !Player.Instance.thing.dead)
        {
            foreach (Skill skill in skillList)
            {
                
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
