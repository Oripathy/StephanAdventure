using System.Collections;
using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotFrozenState : BioBotState
    {
        private bool _isFrozen;
        private float _freezeDuration;

        public BioBotFrozenState(BioBotStateMachine bioBotStateMachine, BioBotEntity bioBot, BioBotData data,
            float duration,
            string animationBoolName) : base(bioBotStateMachine, bioBot, data, animationBoolName)
        {
            _freezeDuration = duration;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isFrozen = true;
            _bioBot.ExecuteCoroutine(Freeze());
        }

        public override BioBotState SetNextState()
        {
            if (!_isFrozen)
                return _stateMachine.PatrolState;

            return this;
        }

        private IEnumerator Freeze()
        {
            yield return new WaitForSeconds(_freezeDuration);
            _isFrozen = false;
        }
    }
}