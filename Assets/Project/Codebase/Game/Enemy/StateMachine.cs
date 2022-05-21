using UnityEngine;
using Game.Towers;
using Game.Damage;
using System.Collections;

namespace Game.Bots.Behaviour
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private MainTower _finalGoal;
        [SerializeField] private Vision _vision;
        [SerializeField] private EnemyAi _intelligence;
        [SerializeField] private StatesContainer _states;
        private State _startState;
        private State _mainState;
        private State _currentState;

        private void OnEnable()
        {
            _vision.PlayerFound += OnPlayerFound;
            _vision.PlayerStay += OnPlayerFound;
            _vision.TowerFound += OnTowerFound;
        }

        private void Start()
        {            
            _startState = Instantiate(_states.Walking);
            _startState.Initialize(_intelligence);

            SetState(_startState);
            StartCoroutine(CheckStateForFinished());
        }

        private void OnDisable()
        {
            _vision.PlayerFound -= OnPlayerFound;
            _vision.PlayerStay -= OnPlayerFound;
            _vision.TowerFound -= OnTowerFound;
        }

        private void FixedUpdate()
        {
            _currentState.Run();
        }

        public void Launch()
        {
            _startState = Instantiate(_states.Walking);
            _startState.Initialize(_intelligence);

            SetState(_startState);
            StartCoroutine(CheckStateForFinished());
        }

        public void ForceSleep()
        {
            BuildAndTryToSetState(_states.Sleeping);
        }

        private void OnPlayerFound(Player player)
        {
            BuildAndTryToSetState(_states.AttackPlayer, player.Transform, player);                        
        }

        private void OnTowerFound(Tower tower)
        {
            BuildAndTryToSetState(_states.AttackTower, tower.Transform, tower);
        }

        private State BuildAndTryToSetState(State state)
        {
            var stateInstance = Instantiate(state);
            stateInstance.Initialize(_intelligence);

            TryToSetState(stateInstance);

            return stateInstance;
        }

        private State BuildAndTryToSetState(State state, Transform targetTransform, IDamageable damageable)
        {
            var stateInstance = Instantiate(state);
            var target = new Target(targetTransform, damageable);

            _intelligence.Target = target;
            stateInstance.Initialize(_intelligence);

            TryToSetState(stateInstance);

            return stateInstance;
        }

        private void TryToSetState(State stateInstance)
        {
            if (_currentState == null)
            {
                SetState(stateInstance);
                return;
            }

            var isStateAvailable = (stateInstance.Priority > _currentState.Priority || _currentState.IsFinished) && stateInstance.CanTransit;

            if (isStateAvailable)
            {
                SetState(stateInstance);
            }
        }

        private void SetState(State newState)
        {
            _currentState?.Drop();
            _currentState = newState;
        }

        private IEnumerator CheckStateForFinished()
        {
            while (true)
            {
                yield return new WaitUntil(() => _currentState.IsFinished);

                OnStateEnded();
            }
        }

        private void OnStateEnded()
        {
            if (_startState.IsFinished == false)
            {
                SetState(_startState);
            }
            else if (_mainState == null)
            {
                _mainState = BuildAndTryToSetState(_states.AttackCastle, _finalGoal.AttackPoint, _finalGoal);
            }
            else
            {
                BuildAndTryToSetState(_states.Sleeping);
            }
        }
    }
}