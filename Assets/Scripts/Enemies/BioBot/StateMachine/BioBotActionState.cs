using System.Collections;
using Stephan;
using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotActionState : BioBotState
    {
        private bool _isActionDone;

        public BioBotActionState(BioBotStateMachine bioBotStateMachine, BioBotEntity bioBot, BioBotData data,
            string animationBoolName) : base(bioBotStateMachine, bioBot, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isActionDone = false;
            _bioBot.ExecuteCoroutine(DoAction());
        }

        public override BioBotState SetNextState()
        {
            if (_isActionDone)
                return _stateMachine.PatrolState;

            return this;
        }

        private IEnumerator DoAction()
        {
            var targetsInRange =
                Physics.OverlapSphere(_bioBot.transform.position, _data.ActionRadius, _bioBot.StephanLayer);

            foreach (var target in targetsInRange)
            {
                if (target.TryGetComponent<IFearApplier>(out var fearApplier))
                    fearApplier.ApplyFear(_data.FearAmountToReceiveDuringAction);
            }

            yield return null;
            _isActionDone = true;
        }
    }
}