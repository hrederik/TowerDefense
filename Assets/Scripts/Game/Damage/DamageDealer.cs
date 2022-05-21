using UnityEngine;
using UnityEngine.Events;
using Game.Bots;
using System.Collections;
using System.Collections.Generic;

namespace Game.Damage
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private float _shootDelay;
        [SerializeField] private MonoBehaviour _attackableBehaviour;
        [SerializeField] private UnityEvent _attacked;
        private List<EnemyAi> _enemies = new List<EnemyAi>();
        private bool _isReady;
        private EnemyAi _target;

        public event UnityAction Attacked;

        public EnemyAi Target
        {
            get
            {
                bool isCurrentTargetAvailable = _target != null && _target.IsDead == false && _enemies.Contains(_target);

                if (isCurrentTargetAvailable)
                {
                    return _target;
                }
                else if (_enemies.Count > 0)
                {
                    _target = _enemies[0];
                    return _enemies[0];
                }
                else
                {
                    _target = null;
                    return null;
                }
            }

            private set => _target = value;
        }
        private IAttackable Attackable => (IAttackable)_attackableBehaviour;

        private void OnValidate()
        {
            if (_attackableBehaviour != null)
                InterfaceValidator.Validate<IAttackable>(_attackableBehaviour);
        }

        private void Start()
        {
            _isReady = true;
            StartCoroutine(Shooting());
        }

        public void EnemyEntered(EnemyAi enemy)
        {
            _enemies.Add(enemy);
        }

        public void EnemyExited(EnemyAi enemy)
        {
            _enemies.Remove(enemy);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitUntil(() => Target != null && _isReady == true);

                if (Target.IsDead == false)
                {
                    Attackable.Attack(Target);
                    _isReady = false;

                    _attacked?.Invoke();
                    Attacked?.Invoke();
                    StartCoroutine(Charging());
                }
                else
                {
                    _enemies.Remove(_target);
                }
            }
        }

        private IEnumerator Charging()
        {
            yield return new WaitForSeconds(_shootDelay);
            _isReady = true;
        }
    }
}