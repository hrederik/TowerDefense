using UnityEngine;
using Game.Damage;

namespace Game.Towers
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _shooter;
        private Vector3 _startRotation;

        private void OnEnable()
        {
            _damageDealer.Attacked += OnShoot;
        }

        private void Awake()
        {
            _startRotation = _shooter.rotation.eulerAngles;
        }

        private void OnDisable()
        {
            _damageDealer.Attacked -= OnShoot;
        }

        private void Update()
        {
            if (_damageDealer.Target != null)
            {
                LookAt(_damageDealer.Target.Transform);
            }
        }

        public void LookAt(Transform target)
        {
            _shooter.LookAt(target);

            var newRotation = _shooter.rotation.eulerAngles;
            newRotation.x = _startRotation.x;
            newRotation.z = _startRotation.z;

            _shooter.rotation = Quaternion.Euler(newRotation);
        }

        private void OnShoot()
        {
            _animator.SetTrigger("Attack");
        }
    }
}