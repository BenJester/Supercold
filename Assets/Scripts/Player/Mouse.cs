using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseMode
{
    Normal, LockOn, Direction
}

public class Mouse : MonoBehaviour
{
    public static Mouse Instance { get; private set; }
    public MouseMode mode;
    private LockOnSkill lockOnSkill;
    private DirectionSkill directionSkill;
    public Texture2D lockCursor;
    public Texture2D normalCursor;
    UIIndicator indicator;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        indicator = Player.Instance.indicator;
    }

    public void LockOnMouse(LockOnSkill skill)
    {
        mode = MouseMode.LockOn;
        lockOnSkill = skill;
    }

    public void SelectDirection(DirectionSkill skill)
    {
        mode = MouseMode.Direction;
        directionSkill = skill;
    }

    void SelectTarget(LockOnSkill skill, GameObject target)
    {
        skill.target = target;
        skill.Cast();
    }

    GameObject MouseSelectGameObj()
    {
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (hit && hit.collider.GetComponent<Thing>() != null && hit.collider.GetComponent<Thing>() != Player.Instance.thing)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }

    void Update()
    {
        HandleInput();
        HandleCursorSprite();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mode == MouseMode.LockOn)
            {
                
                GameObject hit = MouseSelectGameObj();
                if (hit)
                {
                    SelectTarget(lockOnSkill, hit);
                    mode = MouseMode.Normal;
                }
            }
            if (mode == MouseMode.Direction)
            {
                Vector2 pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                directionSkill.targetPos = pos;
                directionSkill.Cast();
                mode = MouseMode.Normal;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            mode = MouseMode.Normal;
            indicator.HideCircle();
        }
    }

    void HandleCursorSprite()
    {
        switch (mode)
        {
            case MouseMode.Normal:
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                
                break;
            case MouseMode.LockOn:
                Cursor.SetCursor(lockCursor, Vector2.zero, CursorMode.Auto);
                indicator.ShowCircle(lockOnSkill);
                break;
            case MouseMode.Direction:
                Cursor.SetCursor(lockCursor, Vector2.zero, CursorMode.Auto);
                indicator.ShowCircle(directionSkill);
                break;
            default:
                break;
        }
    }
}
