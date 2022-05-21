using UnityEngine;
using Game.Bots;
using System.Collections.Generic;

namespace Game.Damage
{
    public class Nucleus : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject _explosionPrefab2;
        [SerializeField] private GameObject _soundsPrefab;
        private List<EnemyAi> _targets;

        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _targets = new List<EnemyAi>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            ApplyDamage();
        }

        public void AddTarget(EnemyAi enemy)
        {
            _targets.Add(enemy);
        }

        public void RemoveTarget(EnemyAi enemy)
        {
            _targets.Remove(enemy);
        }

        private void ApplyDamage()
        {
            foreach (var target in _targets)
            {
                target.ApplyDamage(_damage);
            }

            // TODO: Можно сделать универсальный префаб, на котором будет и система частиц и звук
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Instantiate(_explosionPrefab2, transform.position, Quaternion.identity);

            Instantiate(_soundsPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}