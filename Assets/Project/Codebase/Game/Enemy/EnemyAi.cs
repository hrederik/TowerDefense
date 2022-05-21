using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Game.Stats;
using Game.Damage;
using Game.Movement;
using Game.Bots.Behaviour;
using System.Collections;

namespace Game.Bots
{
    public class EnemyAi : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;
        [SerializeField] private MonoBehaviour _moveableBehaviour;
        [SerializeField] private PathContainer _pathContainer;
        [SerializeField] private StateMachine _stateMachine;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _frozen;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private ParticleSystem _ice;
        [SerializeField] private ParticleSystem _fire;
        [SerializeField] private float _hideDelay;
        [SerializeField] private GameObject _hearthPrefab;
        [SerializeField] private UnityEvent _forceHided;
        private Transform _transform;
        private Coroutine _attack;

        private bool _isFrozen;
        private bool _isBurning;

        public event UnityAction Attacked;
        public event UnityAction Killed;

        public bool IsDead { get; private set; }
        public Transform[] Path => _pathContainer.PathPoints;
        public Transform Transform => _transform;
        public Target Target { get; set; }
        public Vector3 Position => _transform.position;
        private IMoveable _moveable => (IMoveable)_moveableBehaviour;

        private void OnValidate()
        {
            if (_moveableBehaviour != null)
                InterfaceValidator.Validate<IMoveable>(_moveableBehaviour);
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Move(Vector3 direction)
        {
            _moveable.Move(direction);
        }

        public void ApplyDamage(int amount)
        {
            _transform.DOMove(_transform.position - _transform.forward * 1.4f, 0.2f);

            _health.Decrease(amount);            
        }

        public void Kill()
        {
            IsDead = true;
            Killed?.Invoke();

            var isSpawnHeart = Random.Range(0, 100);

            if (isSpawnHeart >= 80)
            {
                Instantiate(_hearthPrefab, _transform.position, Quaternion.identity);
            }

            Sleep();
            StartCoroutine(DealayedHide(_hideDelay));
        }

        public void ForceHide()
        {
            Sleep();
            _renderer.enabled = false;

            _forceHided?.Invoke();
        }

        public void Sleep()
        {
            _moveable.Move(Vector3.zero);
            _stateMachine.ForceSleep();
        }

        public void Freeze()
        {
            var freezeSpeed = 1.5f;
            _moveable.Speed = freezeSpeed;

            _ice.Play();
            _renderer.material = _frozen;

            _isFrozen = true;

            if (gameObject.activeSelf)
                StartCoroutine(Unfreeze(4));
        }

        public void Fire()
        {
            if (gameObject.activeSelf)
                StartCoroutine(Unfreeze(0));

            _fire.Play();
            _isBurning = true;
        }

        public void StartAttack(Target target, int damage, float attackDelay)
        {
            if (_attack == null)
            {
                _attack = StartCoroutine(Attack(target, damage, attackDelay));
            }
        }

        public void StopAttack()
        {
            if (_attack != null)
            {
                StopCoroutine(_attack);
                _attack = null;
            }
        }

        public void OnLaunch()
        {
            _moveable.ResetSpeed();
            _renderer.material = _defaultMaterial;

            _health.Clear();
            _stateMachine.Launch();
        }

        public void Clear()
        {
            IsDead = false;
            _health.Clear();
        }

        private IEnumerator Unfreeze(float duration)
        {
            yield return new WaitForSeconds(duration);

            _renderer.material = _defaultMaterial;
            _moveable.ResetSpeed();

            _isFrozen = false;
        }

        private IEnumerator DealayedHide(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        private IEnumerator Attack(Target target, int damage, float attackDelay)
        {
            while (target.IsDead == false)
            {
                yield return new WaitForSeconds(attackDelay);
                Attacked?.Invoke();
                target.ApplyDamage(damage);
            }
        }
    }
}