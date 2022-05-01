using System.Collections;
using UnityEngine;

namespace Enemies.Kyle.StateMachine
{
    internal class KyleFrozenState : KyleState
    {
        private float _freezeDuration;
        private bool _isFrozen;

        public KyleFrozenState(KyleStateMachine stateMachine, KyleEntity kyle, KyleData data, float duration,
            string animationBoolName) : base(stateMachine, kyle, data, animationBoolName)
        {
            _freezeDuration = duration;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isFrozen = true;
            _kyle.ExecuteCoroutine(Freeze());
        }

        public override KyleState SetNextState()
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