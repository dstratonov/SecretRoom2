using Common.Events;
using Common.UI;
using Common.UI.Layers;
using Cysharp.Threading.Tasks;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Game.UI;

namespace Game.Battle.SubModules.HUD
{
    public class BattleHUDSubModule : IBattleStartedSubModule, IUnitTurnStartedSubModule
    {
        private readonly ViewService _viewService;
        private readonly EventBus _eventBus;

        private BattleHUDView _battleHUD;
        
        public BattleHUDSubModule(ViewService viewService, EventBus eventBus)
        {
            _viewService = viewService;
            _eventBus = eventBus;
        }

        public async UniTask OnBattleStarted(BattleModel model)
        {
            _eventBus.Subscribe<AbilityCastStartEvent>(OnAbilityStartCasted);

            await CreateBattleHUD(model.PlayerTeam);
        }

        private async UniTask CreateBattleHUD(TeamModel playerTeam)
        {
            _battleHUD = await _viewService.ShowView<BattleHUDView, BattleHUDView.Model>(UILayer.HUD, new BattleHUDView.Model
            {
                playerTeam = playerTeam,
            });
        }
        
        private void OnAbilityStartCasted()
        {
            _battleHUD.DeactivateSkillChooser();
        }

        public void OnUnitTurnStarted(BattleUnitModel unit)
        {
            var unitAbilities = unit.GetSystem<AbilitySystem>();
            
            _battleHUD.ActivateSkillChooser(unitAbilities.GetAbilityModels());
        }
    }
}