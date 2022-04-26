using System.Collections;
using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotIdleState : SpheroBotState
    {

        public SpheroBotIdleState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBotEntity spheroBot,
            string animationBoolName) : base(stateMachine, spheroBot, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override SpheroBotState SetNextState()
        {
            return this;
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(0f);
        }
    }
}