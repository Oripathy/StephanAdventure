using System.Collections;
using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotActionState : SpheroBotState
    {
        private bool _isActionDone;

        public SpheroBotActionState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBotEntity spheroBot,
            string animationBoolName) : base(stateMachine, spheroBot, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isActionDone = false;
            _spheroBot.ExecuteCoroutine(DoAction());
        }

        public override SpheroBotState SetNextState()
        {
            if (_isActionDone)
                return _stateMachine.PatrolState;

            return this;
        }

        private IEnumerator DoAction()
        {
            yield return null;
            //write action;
            _isActionDone = true;
        }
    }
}