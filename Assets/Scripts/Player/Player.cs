using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [HideInInspector]
    public UIIndicator indicator;
    [HideInInspector]
    public Thing thing;
    public List<Actor> actors;
    //现在控制的角色
    public Actor currActor;
    CircleCollider2D col;
    Rigidbody2D body;

    public Thing currAITarget;

    public Vector2 dir;
    //public Skill QSkill;
    //public float QSkillCooldown;
    //public Skill ESkill;
    //public float ESkillCooldown;
    ////UI显示用
    //public float curMaxCooldown;
    public int handMaxNum = 6;
    public List<Skill> Deck;
    public List<Skill> Discard;
    public List<Skill> Hand;

    public Skill Reload;
    public Skill Empty;

    public Text targetPosCountdown;

    public int maxMana;
    public int mana;

    public delegate void PlayCardDelegate(Action action);
    public event PlayCardDelegate OnPlayCard;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        indicator = GetComponent<UIIndicator>();
    }

    void Start()
    {
        
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        //Times.Instance.EnterBulletTime();
        thing = GetComponent<Thing>();
        //InitQESkill();

        //Discard = new List<Skill>();
        //Utility.Instance.InitDeck(ref Deck, thing);
        //InitHand();
    }

    void Update()
    {
        HandleMovementInput();
        HandleTime();
        HandleSkillInput();
        HandleUI();
        //HandleCooldown();
    }

    //public void SetCooldown(float cd)
    //{
    //    QSkillCooldown = cd;
    //    ESkillCooldown = cd;
    //}

    //void HandleCooldown()
    //{
    //    if (QSkillCooldown > 0f)
    //    {
    //        QSkillCooldown -= Time.deltaTime;
    //    }
    //    if (QSkillCooldown < 0f)
    //        QSkillCooldown = 0f;

    //    if (ESkillCooldown > 0f)
    //    {
    //        ESkillCooldown -= Time.deltaTime;
    //    }
    //    if (ESkillCooldown < 0f)
    //        ESkillCooldown = 0f;
    //}

    void HandleUI()
    {

    }

    //void InitQESkill()
    //{
    //    QSkill.owner = thing;
    //    ESkill.owner = thing;
    //    SetCooldown(0f);
    //}

    void SetHandToEmpty()
    {
        Hand = new List<Skill>();
        for (int i = 0; i < handMaxNum; i++)
        {
            Hand.Add(Empty);
        }
    }

    public void BroadcastPlayCard(Action action)
    {
        OnPlayCard?.Invoke(action);
    }

    void InitHand()
    {
        Empty.owner = thing;
        Reload.owner = thing;
        SetHandToEmpty();
        DrawCard(handMaxNum);
    }

    void HandleTime()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Times.Instance.targetTimeScale != Times.Instance.bulletTimeScale)
                Times.Instance.EnterBulletTime();
            else
                Times.Instance.ExitBulletTime();
        }

        //if (!Input.GetMouseButton(1))
        //{
        //    Times.Instance.EnterBulletTime();
        //}
        //else
        //    Times.Instance.ExitBulletTime();
    }

    void FixedUpdate()
    {
        
    }

    void HandleSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currActor.QSkillCooldown == 0f)
        {
            currActor.QSkill.OnKey();
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Hand[1].OnKey();
        //}
        if (Input.GetKeyDown(KeyCode.E) && currActor.ESkillCooldown == 0f)
        {
            currActor.ESkill.OnKey();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActor(actors[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActor(actors[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActor(actors[2]);
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Hand[3].OnKey();
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Hand[4].OnKey();
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Hand[5].OnKey();
        //}
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Reload.OnKey();
        //}

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Restart();
        }
    }

    void Restart()
    {
        Time.timeScale = Times.Instance.startTimeScale;
        Time.fixedDeltaTime = Times.Instance.startDeltaTime;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButton(1))
        {
            //thing.targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        dir = new Vector2((Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ?  1f : 0f),
                          (Input.GetKey(KeyCode.W) ?  1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f));
        currActor.Move(dir);

    }

    void SwitchActor(Actor actor)
    {
        if (actor == currActor)
            return;
        actor.GetComponent<ActorAI>().SwitchToCurrActor();
        currActor.GetComponent<ActorAI>().SwitchToAIMode();
        Vector3 tmp = currActor.transform.position;
        currActor.transform.position = actor.transform.position;
        actor.transform.position = tmp;

        currActor = actor;
        
    }

    //private void Move()
    //{
    //    if (!thing.canMove || thing.dead) return;
    //    body.MovePosition(body.position + dir.normalized * thing.speed * Time.fixedDeltaTime);
    //}

    int HandCount()
    {
        int count = 0;
        for (int i = 0; i < Hand.Count; i++)
        {
            if (Hand[i].isCard())
                count += 1;
        }
        return count;
    }

    public void DrawCard(Skill skill)
    {
        if (HandCount() >= handMaxNum)
        {
            Debug.Log("hand is full");
            return;
        }
        InsertCard(skill);
    }

    public void DrawCard(int number)
    {
        for (int i = 0; i < number; i ++)
        {
            if (HandCount() >= handMaxNum)
            {
                Debug.Log("hand is full");
                return;
            }
                
            if (Deck.Count == 0)
                DiscardToDeck();

            int index = Random.Range(0, Deck.Count - 1);
            InsertCard(Deck[index]);
            Deck.RemoveAt(index);
        }
    }

    public void DiscardCard(Skill skill)
    {
        if (!skill.isCard()) return;
        Discard.Add(skill);
        Hand[Hand.IndexOf(skill)] = Empty;
    }

    bool InsertCard(Skill skill)
    {
        for (int i = 0; i < handMaxNum; i++)
        {
            if (!Hand[i].isCard())
            {
                Hand[i] = skill;
                skill.owner = thing;
                return true;
            }
                
        }
        return false;
    }

    void DiscardToDeck()
    {
        Deck = Discard;
        Discard = new List<Skill>();
    }

    public void ReturnHandToDeck()
    {
        foreach (var Skill in Hand)
        {
            if (Skill.isCard())
                Deck.Add(Skill);
        }
        SetHandToEmpty();
    }

    public void RefillMana()
    {
        mana = maxMana;
    }
}
