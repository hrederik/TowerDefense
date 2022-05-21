using UnityEngine;

namespace Game.Bots
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyAi _bot;

        private void OnEnable()
        {
            _bot.Attacked += OnAttack;
            _bot.Killed += OnDeath;
        }

        private void OnDisable()
        {
            _bot.Attacked -= OnAttack;
            _bot.Killed -= OnDeath;
        }

        public void OnAttack()
        {
            _animator.SetTrigger("Attack");
        }

        public void OnDeath()
        {
            _animator.SetTrigger("Death");
        }
    }
}