using Common.Events;
using Common.UI;
using Common.UI.Layers;
using Cysharp.Threading.Tasks;
using Game.Battle.Models;
using Game.UI;

namespace Game.Battle.SubModules.HUD
{
    public class BattleHUDSubModule : IBattleStartedSubModule
    {
        private readonly ViewService _viewService;
        private readonly EventBus _eventBus;

        public BattleHUDSubModule(ViewService viewService, EventBus eventBus)
        {
            _viewService = viewService;
            _eventBus = eventBus;
        }

        public void OnBattleStarted(BattleModel model)
        {
            _viewService.ShowView<BattleHUDView, BattleHUDView.Model>(UILayer.HUD, new BattleHUDView.Model
            {
                playerTeam = model.PlayerTeam,
            }).Forget();
        }
    }
}