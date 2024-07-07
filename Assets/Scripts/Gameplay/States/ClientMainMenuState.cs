using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleGame.Gameplay.State
{
    public class ClientMainMenuState : GameState
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<Entity>(Lifetime.Scoped);
        }
    }
}
