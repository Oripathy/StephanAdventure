using System.Collections;
using UnityEngine;
using Items;

namespace Player.StateMachine
{
    internal class PlayerThrowState : PlayerState
    {
        private Item _item;
        private bool _isThrowDone;

        public PlayerThrowState(PlayerStateMachine stateMachine, PlayerData data, PlayerEntity player, Item item,
            string animationBoolName) : base(stateMachine, data, player, animationBoolName)
        {
            _item = item;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isThrowDone = false;
            _player.ExecuteCoroutine(Throw());
        }

        public override PlayerState SetNextState()
        {
            if (_isThrowDone)
                return _stateMachine.IdleState;

            return this;
        }

        private IEnumerator Throw()
        {
            _item.transform.parent = _player.PalmHoldingPoint;
            _item.transform.position = _player.PalmHoldingPoint.position;
            yield return new WaitForSeconds(0.8f);
            _item.BeThrown();
            _item.Rigidbody.AddForce(_player.transform.forward * _data.ThrowForce, ForceMode.Impulse);
            yield return new WaitForSeconds(1.17f);
            _isThrowDone = true;
        }
    }
}