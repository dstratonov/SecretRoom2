using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class skillChooser : MonoBehaviour
{
    private List<SkillWrapper> skills = new List<SkillWrapper>();
    private Vector2 offset;

    private Vector2 defaultSize = new Vector3(1.0f, 1.0f, 1.0f);

    public SkillWrapper skillItem;

    public Vector2 startPosition;

    public int distanceBetween;
    public float unselectedSizeModifier;

    private int _selectedId = 0;

    void Start()
    {
        var buttonPos = startPosition;
        for (var i = 0; i < 10; i++){
            skills.Add(createNewButton(buttonPos, "skill: " + i));
            buttonPos.y -= distanceBetween;
        }
        UpdateOffset();
        // var newButton = Instantiate(skillItem, this.gameObject.transform);
        // newButton.SetPosition(startPosition);
        // updateSizes();
    }


    // Update is called once per frame
    void Update()
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

    private void UpdateOffset()
    {
        offset = startPosition - skills[_selectedId].GetPosition();
    }

    private SkillWrapper createNewButton(Vector2 buttonPosition, string buttonText)
    {
        var newButton = Instantiate(skillItem, this.gameObject.transform);
        newButton.SetPosition(buttonPosition);
        newButton.SetText(buttonText);
        return newButton;
    }

    void UpdateSizes()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            float distance = Math.Abs(i - _selectedId);
            float coef = Mathf.Pow(unselectedSizeModifier, distance);
            skills[i].SetSize(coef);
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            var currPos = skills[i].GetPosition();
            skills[i].SetSmoothPosition(currPos + offset);
        }
    }
}
