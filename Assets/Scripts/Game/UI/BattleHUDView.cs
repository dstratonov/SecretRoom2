using System.Collections.Generic;
using Common.UI;
using Game.Battle.Abilities;
using Game.Battle.Models;
using Game.Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BattleHUDView : BaseModelView<BattleHUDView.Model>
    {
        public class Model : ViewModel
        {
            public TeamModel playerTeam;
        }

        [SerializeField] private List<Image> _playerSlots = new();
        [SerializeField] private SkillChooser _skillChooser;

        protected override void OnActivate()
        {
            base.OnActivate();

            SetPlayerTeam();
        }

        private void SetPlayerTeam()
        {
            IReadOnlyList<BattleUnitModel> playerUnits = ViewModel.playerTeam.GetUnits();

            for (var i = 0; i < playerUnits.Count; i++)
            {
                _playerSlots[i].sprite = playerUnits[i].GetViewData().unitIcon;
            }
        }

        public void ActivateSkillChooser(IReadOnlyList<AbilityModel> abilities)
        {
            _skillChooser.gameObject.SetActive(true);
            _skillChooser.Init(abilities);
        }

        public void OnAbilityChangedEvent(int newAbilityId)
        {
            _skillChooser.AbilitySelected(newAbilityId);
        }

        public void DeactivateSkillChooser()
        {
            _skillChooser.gameObject.SetActive(false);
        }
    }
}