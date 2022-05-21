using UnityEngine;
using Game.Damage;

namespace Game.Bots
{
    public class Target : IDamageable
    {
        private Transform _transform;
        private IDamageable _damageable;

        public Target(Transform transform, IDamageable damageable)
        {
            _transform = transform;
            _damageable = damageable;
        }

        public Transform Transform => _transform;
        public Vector3 Position => _transform.position;
        public bool IsDead => _damageable.IsDead;

        public void ApplyDamage(int amount)
        {
            _damageable.ApplyDamage(amount);
        }
    }
}