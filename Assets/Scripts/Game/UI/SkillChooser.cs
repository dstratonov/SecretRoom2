using System;
using System.Collections.Generic;
using Game.Battle.Abilities;
using UnityEngine;

namespace Game.UI
{
    public class SkillChooser : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        
        public int distanceBetween;

        public SkillWrapper skillItem;

        public float unselectedOffsetModifier;
        public float unselectedSizeModifier;

        private int _selectedId;

        private Vector2 offset;
        private readonly List<SkillWrapper> skills = new();

        public void Init(IReadOnlyList<AbilityModel> abilityModels)
        {
            Vector2 buttonPos = Vector2.zero;
            
            foreach (AbilityModel ability in abilityModels)
            {
                skills.Add(CreateNewButton(buttonPos, ability.Data.id));
                buttonPos.y -= distanceBetween;
            }

            UpdateOffset();
        }

        private void Update()
        {
            // if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
            // {
            //     _selectedId--;
            // }
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.X))
            // {
            //     _selectedId++;
            // }

            _selectedId = Math.Min(skills.Count - 1, _selectedId);
            _selectedId = Math.Max(0, _selectedId);
            
            UpdateOffset();
            UpdateSizes();
            UpdatePositions();
        }

        private SkillWrapper CreateNewButton(Vector2 buttonPosition, string buttonText)
        {
            SkillWrapper newButton = Instantiate(skillItem, _content);
            newButton.SetPosition(buttonPosition);
            newButton.SetText(buttonText);
            return newButton;
        }

        private void UpdateOffset()
        {
            offset = skills[_selectedId].GetPosition() * -1;
            offset.x = 0;
        }

        private void UpdatePositions()
        {
            for (var i = 0; i < skills.Count; i++)
            {
                float distance = Math.Abs(i - _selectedId);
                Vector2 currPos = skills[i].GetPosition();
                Vector2 newPos = currPos + offset;
                newPos.x = distance * unselectedOffsetModifier;
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
}