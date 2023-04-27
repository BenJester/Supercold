using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAI : MonoBehaviour
{
    // Start is called before the first frame update
    Actor actor;
    Thing thing;
    public List<Skill> skillList;
    public float attackInteval;
    bool init;
    public bool ready;

    public int currIndex;


    void Start()
    {
        actor = GetComponent<Actor>();
        thing = GetComponent<Thing>();
    }

    public void SwitchToCurrActor()
    {
        StopAllCoroutines();
    }

    public void SwitchToAIMode()
    {
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (!init)
        {
            init = true;
            foreach (Skill skill in skillList)
            {
                skill.owner = thing;
            }
            if (Player.Instance.currActor != actor)
                StartCoroutine(Attack());
        }
    }

    Vector3 PredictPosition(Thing thing, Skill skill)
    {
        return thing.transform.position + Random.Range(0.8f, 1.2f) * thing.speed * (thing.canMove ? 1f : 0.1f) *
            ((Vector3)thing.targetPos - thing.transform.position).normalized
            * (skill.preCastTime + skill.postCastTime);
    }

    void DoSkill(Skill skill)
    {

        if (skill.GetType().IsSubclassOf(typeof(ActivateSkill)))
        {
            Action action = new Action(thing, (ActivateSkill)skill);
            action.CastTimeDo();
        }
        else if (skill.GetType().IsSubclassOf(typeof(LockOnSkill)))
        {
            Action action = new Action(thing, (LockOnSkill)skill, Player.Instance.currAITarget.gameObject);
            action.CastTimeDo();
        }
        else if (skill.GetType().IsSubclassOf(typeof(DirectionSkill)))
        {
            Action action = new Action(thing, (DirectionSkill)skill, PredictPosition(Player.Instance.currAITarget.GetComponent<Thing>(), skill));
            action.CastTimeDo();
        }
        ready = true;
    }

    IEnumerator Attack()
    {
        while (!thing.dead && skillList.Count > 0)
        {
            while (Player.Instance.currAITarget == null)
            {
                yield return new WaitForEndOfFrame();
            }
                
            currIndex = 0;
            foreach (Skill skill in skillList)
            {
                if (thing.dead) break;
                while (skill.range != 0 && Vector3.Distance(transform.position, Player.Instance.currAITarget.transform.position) > skill.range)
                {
                    thing.targetPos = Player.Instance.currAITarget.transform.position 
                        + (transform.position - Player.Instance.currAITarget.transform.position).normalized.normalized * skill.range * 0.5f 
                        + new Vector3(Random.Range(-skill.range * 0.5f, skill.range * 0.5f), Random.Range(-skill.range * 0.5f, skill.range * 0.5f), 0f);
                    yield return new WaitForEndOfFrame();
                }
                DoSkill(skill);
                yield return new WaitForSeconds(skill.preCastTime + skill.postCastTime);
                currIndex += 1;
                yield return new WaitForSeconds(attackInteval);

            }
        }
    }

    bool IsAIMode()
    {
        return actor != Player.Instance.currActor;
    }

    void MoveToTarget()
    {
        if (Player.Instance.currAITarget == null)
        {
            thing.targetPos = Player.Instance.currAITarget.transform.position;
        }
    }
}
