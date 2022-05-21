using UnityEngine;

namespace Game.Bots.Behaviour
{
    public class StatesContainer : MonoBehaviour
    {
        [SerializeField] private State _walking;
        [SerializeField] private State _attackPlayer;
        [SerializeField] private State _attackTower;
        [SerializeField] private State _attackCastle;
        [SerializeField] private State _sleeping;

        public State Walking => _walking;
        public State AttackPlayer => _attackPlayer;
        public State AttackTower => _attackTower;
        public State AttackCastle => _attackCastle;
        public State Sleeping => _sleeping;
    }
}