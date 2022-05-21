using UnityEngine;
using Game.Bots;

namespace Game.Damage
{
    public class TargetAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private int _damage;
        [SerializeField] private Projectile _projectilePrefab;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Attack(EnemyAi enemy)
        {
            SpawnBullet(enemy);
        }

        private void SpawnBullet(EnemyAi enemy)
        {
            var bullet = Instantiate(_projectilePrefab, _transform.position, Quaternion.identity);
            bullet.Initialize(enemy);
        }
    }
}