using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIndicator : MonoBehaviour
{
    public SpriteRenderer rangeIndicator;
    public SpriteRenderer radiusIndicator;

    private void Start()
    {
        radiusIndicator.transform.parent = null;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        radiusIndicator.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }

    public void ShowCircle(Skill skill)
    {
        if (skill.skillType == SkillType.Activate)
        {
            rangeIndicator.size = Radius(skill.radius);
            radiusIndicator.size = Radius(0f);
        }
        else
        {
            rangeIndicator.size = Radius(skill.range);
            radiusIndicator.size = Radius(skill.radius);
        }

    }

    Vector2 Radius(float radius)
    {
        return new Vector2(radius * 2f, radius * 2f);
    }

    public void HideCircle()
    {
        rangeIndicator.size = Radius(0f);
        radiusIndicator.size = Radius(0f);
    }
}
