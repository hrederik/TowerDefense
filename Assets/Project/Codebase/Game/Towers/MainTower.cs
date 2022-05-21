using UnityEngine;
using Game.Stats;
using Game.Damage;

namespace Game.Towers
{
    public class MainTower : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _attackPoint;

        public bool IsDead { get; private set; }
        public Transform AttackPoint => _attackPoint;

        private void OnEnable()
        {
            _health.Ended += Kill;
        }

        private void OnDisable()
        {
            _health.Ended -= Kill;
        }

        public void ApplyDamage(int amount)
        {
            _health.Decrease(amount);
        }

        private void Kill()
        {
            IsDead = true;
        }
    }
}