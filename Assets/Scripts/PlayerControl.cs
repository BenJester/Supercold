using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }

    public float speed;

    CircleCollider2D col;
    Rigidbody2D rb;

    public Vector2 targetPos;
    public float snapDistance;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovementInput();
    }

    void FixedUpdate()
    {
        GoToTargetPos();
    }

    void GoToTargetPos()
    {
        if (rb.position == targetPos) return;

        Vector2 dir = (targetPos - rb.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, targetPos) <= snapDistance)
        {
            rb.position = targetPos;
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
