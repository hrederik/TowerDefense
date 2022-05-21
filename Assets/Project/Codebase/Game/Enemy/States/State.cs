using UnityEngine;

namespace Game.Bots.Behaviour
{
    public abstract class State : ScriptableObject
    {
        [SerializeField] private int _priority;
        private EnemyAi _character;

        public bool CanTransit { get; protected set; }
        public bool IsFinished { get; protected set; }
        public int Priority => _priority;
        protected EnemyAi Character => _character;

        public virtual void Initialize(EnemyAi character)
        {
            CanTransit = true;
            _character = character;
        }
        public abstract void Run();
        public virtual void Drop() { }
    }
}