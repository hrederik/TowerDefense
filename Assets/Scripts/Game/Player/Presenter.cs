using UnityEngine;
using Game.Damage;
using Game.Movement;
using Players.Systems;

namespace Game.Players
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private DefaultMovement _movement;
        [SerializeField] private TreeCutter _treeCutter;

        private void OnEnable()
        {
            _damageDealer.Attacked += Attack;
            _treeCutter.Cut += Attack;

            _movement.MovementStarted += OnMovementStarted;
            _movement.MovementStopped += OnMovementStopped;
        }

        private void OnDisable()
        {
            _damageDealer.Attacked -= Attack;
            _treeCutter.Cut -= Attack;

            _movement.MovementStarted -= OnMovementStarted;
            _movement.MovementStopped -= OnMovementStopped;
        }

        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public void OnMovementStarted()
        {
            _animator.SetBool("IsWalk", true);
        }

        public void OnMovementStopped()
        {
            _animator.SetBool("IsWalk", false);
        }
    }
}