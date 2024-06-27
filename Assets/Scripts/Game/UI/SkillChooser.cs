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

        public float unselectedOffsetModifier = -20.0f;
        public float unselectedSizeModifier;

        private int _selectedId;

        private Vector2 offset;
        private readonly List<SkillWrapper> skills = new();

        public void Init(IReadOnlyList<AbilityModel> abilityModels)
        {
            Vector2 buttonPos = Vector2.zero;
            
            for (var i = 0; i < abilityModels.Count; i++)
            {
                SkillWrapper skillWrapper;
                
                if (skills.Count >= i)
                {
                    skillWrapper = CreateNewButton(Vector2.zero, string.Empty);
                    skills.Add(skillWrapper);
                }
                else
                {
                    skillWrapper = skills[i];
                    skillWrapper.gameObject.SetActive(true);
                }
                
                UpdateButton(buttonPos, abilityModels[i].Data.id, i);
                buttonPos.y -= distanceBetween;
            }

            for (int i = abilityModels.Count; i < skills.Count; i++)
            {
                skills[i].gameObject.SetActive(false);
            }
            
            UpdateOffset();
        }

        public void AbilitySelected(int abilityId)
        {
            print(abilityId);
            print(skills.Count);
            _selectedId = abilityId;
            
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

        private void UpdateButton(Vector2 buttonPosition, string buttonText, int id)
        {
            skills[id].SetPosition(buttonPosition);
            skills[id].SetText(buttonText);
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