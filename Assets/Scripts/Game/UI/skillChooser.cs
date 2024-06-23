using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class skillChooser : MonoBehaviour
{
    public List<RectTransform> skills;

    public float unselectedSizeModifier;
    private Vector2 defaultSize = new Vector3(1.0f, 1.0f, 1.0f);

    private int _selectedId = 0;

    public float lerpCoef = 0.7f;

    void Start()
    {
        updateSizes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _selectedId--;
            if (_selectedId < 0)
            {
                _selectedId = skills.Count - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _selectedId++;
            if (_selectedId >= skills.Count)
            {
                _selectedId = 0;
            }
        }

        updateSizes();
    }

    void updateSizes()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (i != _selectedId){
                skills[i].localScale = Vector3.Lerp(skills[i].localScale, defaultSize * unselectedSizeModifier, lerpCoef);
            }
            else{
                skills[i].localScale = Vector3.Lerp(skills[i].localScale, defaultSize, lerpCoef);
            }
        }
    }
}
