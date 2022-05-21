using System.Collections.Generic;
using UnityEngine;
using Game.Bots;

namespace Game.Damage
{
    public class FireBall : Projectile
    {
        [SerializeField] private GameObject _particle;
        private List<EnemyAi> _targets;

        private void Start()
        {
            _targets = new List<EnemyAi>();
        }

        public void OnEnemyEntered(EnemyAi enemy)
        {
            _targets.Add(enemy);
        }

        public void OnEnemyExited(EnemyAi enemy)
        {
            _targets.Remove(enemy);
        }

        protected override void ApplyDamage()
        {
            foreach (var target in _targets)
            {
                target.Fire();
                target.ApplyDamage(Damage);
            }

            Instantiate(_particle, transform.position, Quaternion.identity);
            Instantiate(SoundsPrefab, transform.position, Quaternion.identity);

            Target.ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }
}