using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillChooser : MonoBehaviour
{
    public int distanceBetween;

    public SkillWrapper skillItem;

    public Vector2 startPosition;
    public float unselectedOffsetModifier;
    public float unselectedSizeModifier;

    private int _selectedId;

    private Vector2 defaultSize = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector2 offset;
    private readonly List<SkillWrapper> skills = new();

    private void Start()
    {
        Vector2 buttonPos = startPosition;
        for (var i = 0; i < 10; i++)
        {
            skills.Add(createNewButton(buttonPos, "skill: " + i));
            buttonPos.y -= distanceBetween;
        }

        UpdateOffset();
        // var newButton = Instantiate(skillItem, this.gameObject.transform);
        // newButton.SetPosition(startPosition);
        // updateSizes();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _selectedId--;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _selectedId++;
        }

        _selectedId = Math.Min(skills.Count - 1, _selectedId);
        _selectedId = Math.Max(0, _selectedId);
        UpdateOffset();
        UpdateSizes();
        UpdatePositions();
    }

    private SkillWrapper createNewButton(Vector2 buttonPosition, string buttonText)
    {
        SkillWrapper newButton = Instantiate(skillItem, gameObject.transform);
        newButton.SetPosition(buttonPosition);
        newButton.SetText(buttonText);
        return newButton;
    }

    private void UpdateOffset()
    {
        offset = startPosition - skills[_selectedId].GetPosition();
        offset.x = 0;
    }

    private void UpdatePositions()
    {
        for (var i = 0; i < skills.Count; i++)
        {
            float distance = Math.Abs(i - _selectedId);
            Vector2 currPos = skills[i].GetPosition();
            Vector2 newPos = currPos + offset;
            newPos.x = startPosition.x + distance * unselectedOffsetModifier;
            skills[i].SetSmoothPosition(newPos);
        }
    }

    private void UpdateSizes()
    {
        for (var i = 0; i < skills.Count; i++)
        {
            float distance = Math.Abs(i - _selectedId);
            float coef = Mathf.Pow(unselectedSizeModifier, distance);
            skills[i].SetSize(coef);
        }
    }
}