using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotActionState : SpheroBotState
    {
        private Transform _target;
        private Vector3 _previousPosition;
        private float _actionEndTime;
        private bool _isActionDone;

        public SpheroBotActionState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBotEntity spheroBot,
            string animationBoolName, Transform target) : base(stateMachine, spheroBot, data, animationBoolName)
        {
            _target = target;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isActionDone = false;
            _previousPosition = _spheroBot.transform.position;
            _actionEndTime = Time.time + _data.ActionDuration;
            _spheroBot.Rigidbody.isKinematic = true;
            _spheroBot.NavMeshAgent.enabled = true;
            _spheroBot.ExecuteCoroutine(DoAction());
        }

        public override void OnExit()
        {
            _spheroBot.Animator.SetBool("Move", false);       
            _spheroBot.NavMeshAgent.enabled = false;
            _spheroBot.Rigidbody.isKinematic = false;
        }

        public override SpheroBotState SetNextState()
        {
            if (_isActionDone)
                return _stateMachine.PatrolState;

            return this;
        }

        private void MoveTowardsTarget(Vector3 target) => _spheroBot.NavMeshAgent.SetDestination(target);

        private IEnumerator DoAction()
        {
            MoveTowardsTarget(_target.position);

            while (Time.time < _actionEndTime)
            {
                MoveTowardsTarget(_target.position);
                yield return null;
            }
            
            _spheroBot.Animator.SetBool(_animationBoolName, false);
            _spheroBot.Animator.SetBool("Open", true);
            yield return new WaitForSeconds(0.87f);
            _spheroBot.Animator.SetBool("Open", false);
            _spheroBot.Animator.SetBool("Move", true);
            MoveTowardsTarget(_previousPosition);

            while (Vector3.Distance(_spheroBot.transform.position, _previousPosition) >= 0.5f)
            {
                yield return null;
            }
            
            _isActionDone = true;
        }
    }
}