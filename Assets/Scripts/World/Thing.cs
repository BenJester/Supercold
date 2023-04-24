﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Thing : MonoBehaviour
{
    public Actor actor;
    public bool active;
    public bool dead;
    public int team;
    public int maxHp;
    public int hp;
    public int shield;
    //max弱点次数
    public int maxWeakPoint;
    //弱点次数
    public int weakPoint;
    //弱点列表
    public List<WeaknessType> weaknessTypeList;
    //破防时长
    public float breakDur;
    //已破防
    public bool broken;

    public bool invinsible;

    public float rawSpeed;
    public float speedMultiplier = 1f;
    public float speed;
    public Vector2 targetPos;
    public List<Buff> buffList;

    public int strength;

    public float snapDistance = 10f;
    public bool canMove = true;
    public bool canCast = true;
    public bool stunned = false;
    public ThingParticle particle;

    public CircleCollider2D col;
    public Rigidbody2D body;
    SpriteRenderer sprite;

    public Action lastCastAction;
    public Queue<Action> buffer;
    public Queue<StatsBuff> tmpShieldQueue;

    UIBuff buffUI;
    public HPText HPCanvas;
    LineRenderer lr;

    public delegate void LoseHP(int lossHP);
    public event LoseHP OnLoseHP;

    public delegate void GetHit(Thing attacker);
    public event GetHit OnGetHit;

    void Start()
    {
        hp = maxHp;
        weakPoint = maxWeakPoint;
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        targetPos = transform.position;
        particle = GetComponent<ThingParticle>();
        buffer = new Queue<Action>();
        InitBuffs();
        InitUI();
    }

    void InitBuffs()
    {
        List<Buff> newBuffList = new List<Buff>();
        foreach (var buff in buffList)
        {
            Buff newBuff = Instantiate(buff);
            newBuff.Init(this);
            newBuffList.Add(newBuff);
        }
        buffList = newBuffList;
    }

    void InitUI()
    {
        var HPCanvasObj = Instantiate(Prefabs.Instance.HPCanvas, transform);
        HPCanvas = HPCanvasObj.GetComponent<HPText>();
        HPCanvas.thing = this;
        lr = GetComponent<LineRenderer>();
        //GameObject BuffCanvas = Instantiate(Prefabs.Instance.BuffCanvas, transform);
        //buffUI = BuffCanvas.GetComponent<UIBuff>();
        //buffUI.thing = this;
    }

    void Update()
    {
        HandleBuff();
        speed = rawSpeed * Mathf.Clamp(speedMultiplier, 0f, 15f);
        if (lr != null)
        {
            //lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, targetPos);
        }
    }

    void HandleBuff()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].Tick();
            int num = buffList[i].UINum();
            if (num != -1)
            {
                //buffUI.text.text = num.ToString();
            }
        }
    }

    public void NextInBuffer()
    {
        if (buffer.Count > 0 && canCast)
        {
            buffer.Dequeue().CastTimeDo();
        }
    }

    void FixedUpdate()
    {
        GoToTargetPos();
    }

    public void TakeDamage(List<WeaknessType> weaknessList, int damage, Thing owner)
    {
        if (dead) return;
        OnGetHit?.Invoke(owner);
        if (damage == 0 || CheckStackableBool<InvinsibleBuff>()) return;

        // 处理弱点
        if (team != 0)
        {
            foreach (WeaknessType weakness in weaknessList)
            {
                foreach (WeaknessType myWeakness in weaknessTypeList)
                {
                    if (weakness == WeaknessType.ForceWeakness || weakness == myWeakness)
                    {
                        if (!broken && weakPoint > 0)
                        {
                            weakPoint -= 1;
                        }
                        if (weakPoint == 0 && !broken)
                        {
                            Break();
                        }
                    }
                }
            }
        }
        

        damage = Mathf.Clamp(damage + owner.strength, 0, 10000);
        // if tmpShieldQueue
        if (shield > 0)
        {
            int dmgOnShield = Mathf.Min(shield, damage);
            shield -= dmgOnShield;
            damage -= dmgOnShield;
        }
            
        hp -= damage;
        if (damage > 0)
            OnLoseHP?.Invoke(damage);
        if (hp <= 0)
            Die();
    }

    private void Break()
    {
        AddBuff(Utility.Instance.breakBuff);
        broken = true;
    }

    public void RestoreWeakpoint()
    {
        broken = false;
        weakPoint = maxWeakPoint;
    }


    public bool CheckStackableBool<T>()
    {
        foreach (Buff buff in buffList)
        {
            if (buff.GetType() == typeof(T) && buff.active)
                return true;
        }
        return false;
    }

    public void Die()
    {
        dead = true;
        //col.enabled = false;
        sprite.enabled = false;
    }

    public void AddBuff(Buff buff)
    {
        buff = Instantiate(buff);
        buffList.Add(buff);
        buff.Init(this);
        buff.Do();
    }

    void GoToTargetPos()
    {
        if (body.position == targetPos || !canMove || dead || team == 0) return;

        //buffer = new Queue<Action>();
        Vector2 dir = (targetPos - body.position).normalized;
        body.MovePosition(body.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(body.position, targetPos) <= snapDistance * speedMultiplier)
        {
            body.position = targetPos;
        }
    }

}
