using UnityEngine;
using Players.Systems;
using Game.Stats;
using Game.Damage;
using Game.Towers.States;
using System.Collections;

namespace Game.Towers
{
    public class Tower : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _cost;
        [SerializeField] private State _startState;
        [SerializeField] private Health _health;
        [SerializeField] private float _repairingDelay;
        [SerializeField] private Inventory _inventory;
        private Transform _transform;
        private Coroutine _repairing;

        public bool IsDead { get; private set; }
        public Transform Transform => _transform;
        public Vector3 Position => _transform.position;
        public State State { get; set; }
        public int Cost => _cost;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            State = _startState;
            if (_inventory != null)
            {
                State.Initialize(_inventory);
            }
        }

        public void Initialize(Inventory inventory)
        {
            State = _startState;
            State.Initialize(inventory);
        }

        public void ApplyDamage(int amount)
        {
            _health.Decrease(amount);
        }

        public void StartRepairing()
        {
            StopRepairing();
            _repairing = StartCoroutine(Repairing());
        }

        public void StopRepairing()
        {
            if (_repairing != null)
            {
                StopCoroutine(_repairing);
                _repairing = null;
            }
        }

        private IEnumerator Repairing()
        {
            do
            {
                yield return new WaitForSeconds(_repairingDelay);
                State.Do();
            }
            while (State.CanDo);
        }
    }
}