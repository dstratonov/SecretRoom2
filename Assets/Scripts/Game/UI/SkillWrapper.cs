using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillWrapper : MonoBehaviour
{
    public TMP_Text skillText;

    [SerializeField] private RectTransform rectTransform;

    private Vector3 newSize;
    private Vector2 newPosition;
    [SerializeField] private Vector2 defaultSize;
    void Start()
    {
        newSize = new Vector3(defaultSize.x, defaultSize.y, 1.0f);
        rectTransform.localScale = newSize;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, newSize, 0.3f);
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, new Vector3(newPosition.x, newPosition.y, 0), 0.3f);
    }

    public void SetText(string newText)
    {
        skillText.text = newText;
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0);
        newPosition = rectTransform.anchoredPosition;
    }

    public Vector2 GetPosition()
    {
        var currPos = rectTransform.anchoredPosition;
        return new Vector2(currPos.x, currPos.y);
    }

    public void SetSmoothPosition(Vector2 position)
    {
        newPosition = new Vector3(position.x, position.y, 0);
    }

    public void SetSize(float sizeCoef)
    {
        newSize = new Vector3(defaultSize.x * sizeCoef, defaultSize.y, 1.0f);
    }
}
