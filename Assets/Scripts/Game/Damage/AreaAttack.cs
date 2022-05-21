using Game.Bots;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Damage
{
    public class AreaAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private float _force;
        [SerializeField] private Nucleus _nucleusPrefab;
        [SerializeField] private UnityEvent _attacked;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Attack(EnemyAi enemy)
        {
            LaunchNucleus(enemy);
        }

        private void LaunchNucleus(EnemyAi enemy)
        {
            var nucleus = Instantiate(_nucleusPrefab, _transform.position, Quaternion.identity);
            var force = _force * Vector3.Distance(_transform.position, enemy.Position);

            _attacked?.Invoke();
            nucleus.Rigidbody.AddForce(_transform.forward * force);
        }
    }
}