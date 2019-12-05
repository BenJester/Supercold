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
    private LockOnSkill activeSkill;
    private DirectionSkill directionSkill;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void LockOnMouse(LockOnSkill skill)
    {
        mode = MouseMode.LockOn;
        activeSkill = skill;
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

        if (hit)
        {
            Debug.Log(hit.transform.name);
            return hit.transform.gameObject;
        }
        else return null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mode == MouseMode.LockOn)
            {
                GameObject hit = MouseSelectGameObj();
                if (hit)
                {
                    SelectTarget(activeSkill, hit);
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
        
    }
}
