using UnityEngine;
using BattleGame.Gameplay.Service;
using VContainer;
using VContainer.Unity;

namespace BattleGame.Gameplay.State
{
    public class ClientMainMenuState : GameState
    {
        [SerializeField] private NetworkModePopup networkModePopup;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<Entity>(Lifetime.Scoped);
            builder.Register<ConnectPlayerService>(Lifetime.Scoped);
            builder.RegisterComponent(networkModePopup);
        }
    }
}
