using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine
{
    internal class FrozenState : State
    {
        private float _freezeDuration;
        private bool _isFrozen;

        public FrozenState(global::Enemies.StateMachine.StateMachine stateMachine, Enemy enemy, EnemyData data, float duration,
            string animationBoolName) : base(stateMachine, enemy, data, animationBoolName)
        {
            _freezeDuration = duration;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isFrozen = true;
            _enemy.ExecuteCoroutine(Freeze());
        }

        public override State SetNextState()
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