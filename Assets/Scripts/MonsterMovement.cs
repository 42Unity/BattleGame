using System;
using BattleGame.Model;
using Fusion;
using UnityEngine;

namespace BattleGame
{
    public class MonsterMovement : CharacterMovement
    {
        // private CharacterService characterService;
        // TODO: DI
        public static CharacterService characterService => BasicSpawner.characterService;
        private MonsterBehaviour monsterBehaviour;
        [Networked] private Monster.State State { get; set; }
        private Monster Monster => monsterBehaviour.Monster;
        private Character TraceTarget => Monster.TraceTarget;
        private const float AttackRange = 1.0f;

        protected override void Awake()
        {
            base.Awake();
            monsterBehaviour = GetComponent<MonsterBehaviour>();
            monsterBehaviour.OnSpawned += Spawned;
        }

        private void Spawned(Character character)
        {
            Monster.StartPosition = character.Position;
        }

        private void SetState(Monster.State state)
        {
            Monster.CurrentState = state;
            State = state;
        }

        public override void FixedUpdateNetwork()
        {
            // get player nearest to monster in players list
            if (characterService.Players.Count > 0)
            {
                Monster.TraceTarget = characterService.Players[0];
                foreach (var player in characterService.Players)
                {
                    var toPlayer = player.Position - Monster.Position;
                    var toTraceTarget = Monster.TraceTarget.Position - Monster.Position;
                    if (toPlayer.magnitude < toTraceTarget.magnitude)
                    {
                        Monster.TraceTarget = player;
                    }
                }
            }

            if (HasStateAuthority)
            {
                if (State == Monster.State.Idle)
                {
                    if (TraceTarget != null)
                    {
                        var toTarget = TraceTarget.Position - Monster.Position;
                        if (toTarget.magnitude < Monster.SightRange) SetState(Monster.State.Trace);
                    }
                }
                if (State == Monster.State.Trace)
                {
                    if (TraceTarget != null)
                    {
                        var toTarget = TraceTarget.Position - Monster.Position;
                        if (toTarget.magnitude > AttackRange) Move(toTarget);
                    }
                    var toStartPosition = Monster.StartPosition - Monster.Position;
                    if (toStartPosition.magnitude > Monster.TraceDistance) SetState(Monster.State.Back);
                }
                if (State == Monster.State.Back)
                {
                    var toStartPosition = Monster.StartPosition - Monster.Position;
                    if (toStartPosition.magnitude > 0.1) Move(toStartPosition);
                    else SetState(Monster.State.Idle);
                }
            }
            base.FixedUpdateNetwork();
        }
    }
}
