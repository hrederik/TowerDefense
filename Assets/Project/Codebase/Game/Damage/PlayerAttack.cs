using Game.Bots;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Damage
{
    public class PlayerAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private int _damage;
        private List<EnemyAi> _enemies;

        private void Start()
        {
            _enemies = new List<EnemyAi>();
        }

        public void AddEnemy(EnemyAi enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyAi enemy)
        {
            _enemies.Remove(enemy);
        }

        public void Attack(EnemyAi enemy)
        {
            if (_enemies.Count > 0)
            {
                foreach (var item in _enemies)
                {
                    item.ApplyDamage(_damage);
                }
            }
        }
    }
}