using UnityEngine;
using Game.Bots;

namespace Game.Damage
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _minDistance;
        [SerializeField] private GameObject _soundsPrefab;
        private EnemyAi _target;
        private Transform _transform;

        protected GameObject SoundsPrefab => _soundsPrefab;
        protected int Damage => _damage;
        protected EnemyAi Target => _target;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.Position, Time.fixedDeltaTime * _speed);
            _transform.LookAt(_target.Transform);
            CheckForReach();
        }

        public void Initialize(EnemyAi enemy)
        {
            _target = enemy;
        }

        private void CheckForReach()
        {
            var isReached = Vector3.Distance(_transform.position, _target.Position) < _minDistance;

            if (isReached == true)
            {
                ApplyDamage();
            }
        }

        protected abstract void ApplyDamage();
    }
}