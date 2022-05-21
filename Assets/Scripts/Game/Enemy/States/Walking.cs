using UnityEngine;

namespace Game.Bots.Behaviour
{
    [CreateAssetMenu(fileName = "Walking", menuName = "EnemyStates/Walking")]
    public class Walking : State
    {
        private Transform[] _path;
        private int _pathPointIndex;
        private float _reachDistance = 1f;
        private Transform _targetPoint;

        public override void Initialize(EnemyAi character)
        {
            base.Initialize(character);

            _path = character.Path;
            ChangeTargetPoint();
        }

        public override void Run()
        {
            Character.Move(_targetPoint.position - Character.Position);
            CheckReachPoint();
        }

        public override void Drop()
        {
            Character.Move(Vector3.zero);
        }

        private void CheckReachPoint()
        {
            var distance = Vector3.Distance(Character.Position, _targetPoint.position);

            if (distance <= _reachDistance)
            {
                TryToChangeTargetPoint();
            }
        }

        private void TryToChangeTargetPoint()
        {
            bool hasNextPoint = _pathPointIndex < _path.Length;

            if (hasNextPoint)
            {
                ChangeTargetPoint();
            }
            else
            {
                IsFinished = true;
            }
        }

        private void ChangeTargetPoint()
        {
            _targetPoint = _path[_pathPointIndex];
            _pathPointIndex++;

            Character.Transform.LookAt(_targetPoint);
        }
    }
}