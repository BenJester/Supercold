using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [HideInInspector]
    public UIIndicator indicator;
    public Thing thing;
    CircleCollider2D col;
    Rigidbody2D body;

    public Vector2 dir;
    public Skill QSkill;
    public float QSkillCooldown;
    public Skill ESkill;
    public float ESkillCooldown;
    //UIœ‘ æ”√
    public float curMaxCooldown;



    void Start()
    {
        indicator = GetComponent<UIIndicator>();
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        //Times.Instance.EnterBulletTime();
        thing = GetComponent<Thing>();
        thing.actor = this;
        InitQESkill();

        //Discard = new List<Skill>();
        //Utility.Instance.InitDeck(ref Deck, thing);
        //InitHand();
    }

    void Update()
    {
        HandleCooldown();
    }

    public void SetCooldown(float cd)
    {
        QSkillCooldown = cd;
        ESkillCooldown = cd;
    }

    void HandleCooldown()
    {
        if (QSkillCooldown > 0f)
        {
            QSkillCooldown -= Time.deltaTime;
        }
        if (QSkillCooldown < 0f)
            QSkillCooldown = 0f;

        if (ESkillCooldown > 0f)
        {
            ESkillCooldown -= Time.deltaTime;
        }
        if (ESkillCooldown < 0f)
            ESkillCooldown = 0f;
    }


    void InitQESkill()
    {
        QSkill.owner = thing;
        ESkill.owner = thing;
        SetCooldown(0f);
    }



    public void Move(Vector2 dir)
    {
        if (!thing.canMove || thing.dead) return;
        body.MovePosition(body.position + dir.normalized * thing.speed * Time.fixedDeltaTime);
    }

}
