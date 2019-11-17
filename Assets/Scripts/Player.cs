using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float speed;

    CircleCollider2D col;
    Rigidbody2D body;

    public Vector2 targetPos;
    public float snapDistance;

    public float bulletTimeScale = 0.1f;
    float originalTimeScale;

    public Skill qSkill;
    public Skill wSkill;
    public Skill eSkill;
    public Skill rSkill;
    public Skill dSkill;
    public Skill fSkill;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        originalTimeScale = Time.timeScale;
    }

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
        Times.Instance.SetTimeScale(bulletTimeScale);
    }

    void Update()
    {
        HandleMovementInput();
        HandleTime();
    }

    void HandleTime()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Times.Instance.SetTimeScale(bulletTimeScale);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Times.Instance.SetTimeScale(originalTimeScale);
        }
    }

    void FixedUpdate()
    {
        GoToTargetPos();
    }

    void GoToTargetPos()
    {
        if (body.position == targetPos) return;

        Vector2 dir = (targetPos - body.position).normalized;
        body.MovePosition(body.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(body.position, targetPos) <= snapDistance)
        {
            body.position = targetPos;
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);            
        }
    }
}
