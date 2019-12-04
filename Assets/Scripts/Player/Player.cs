using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Thing thing;
    
    CircleCollider2D col;
    Rigidbody2D body;

    public int handMaxNum = 6;
    public List<Skill> Deck;
    public List<Skill> Discard;
    public List<Skill> Hand;

    public Skill Reload;
    public Skill Empty;

    public int maxMana;
    public int mana;

    public delegate void PlayCardDelegate(Action action);
    public event PlayCardDelegate OnPlayCard;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        Times.Instance.EnterBulletTime();
        thing = GetComponent<Thing>();
        
        Discard = new List<Skill>();
        InitHand();
    }

    void Update()
    {
        HandleMovementInput();
        HandleTime();
        HandleSkillInput();       
    }

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
        if (OnPlayCard != null)
            OnPlayCard(action);
    }

    void InitHand()
    {
        SetHandToEmpty();
        DrawCard(handMaxNum);
        Empty.owner = thing;
    }

    void HandleTime()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Times.Instance.EnterBulletTime();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Times.Instance.ExitBulletTime();
        }
    }

    void FixedUpdate()
    {
        
    }

    void HandleSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Hand[0].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Hand[1].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Hand[2].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Hand[3].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Hand[4].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Hand[5].OnKey();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Reload.OnKey();
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            thing.targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

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
            else
            {
                int index = Random.Range(0, Deck.Count - 1);
                InsertCard(Deck[index]);
                Deck.RemoveAt(index);
            }
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
