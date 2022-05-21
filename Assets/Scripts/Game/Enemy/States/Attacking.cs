using UnityEngine;

namespace Game.Bots.Behaviour
{
    [CreateAssetMenu(fileName = "Attacking", menuName = "EnemyStates/Attacking")]
    public class Attacking : State
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _cessationDistance;
        private Target _target;

        public override void Initialize(EnemyAi character)
        {
            base.Initialize(character);

            _target = character.Target;
            CanTransit = Vector3.Distance(Character.Position, _target.Position) <= _cessationDistance;
        }

        public override void Run()
        {
            if (_target.IsDead == true)
            {
                IsFinished = true;
                Character.StopAttack();
                return;
            }

            var distance = Vector3.Distance(Character.Position, _target.Position);

            if (distance <= _attackDistance)
            {
                Character.Move(Vector3.zero);
                Character.StartAttack(_target, _damage, _attackDelay);
            }
            else if (distance <= _cessationDistance)
            {
                Character.StopAttack();
                Character.Transform.LookAt(_target.Transform);
                Character.Move(_target.Position - Character.Position);
            }
            else
            {
                IsFinished = true;
            }
        }

        public override void Drop()
        {
            Character.StopAttack();
            Character.Move(Vector3.zero);
        }
    }
}