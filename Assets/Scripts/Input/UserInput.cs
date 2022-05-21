using UnityEngine;
using Game.Movement;

namespace CustomUserInput
{
    public class UserInput : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _moveableBehaviour;
        private IMoveable _moveable => (IMoveable)_moveableBehaviour;

        [SerializeField] private MonoBehaviour _inputableBehaviour;
        private IInputable _inputable => (IInputable)_inputableBehaviour;

        private void OnValidate()
        {
            if (_moveableBehaviour != null)
                InterfaceValidator.Validate<IMoveable>(_moveableBehaviour);

            if (_inputableBehaviour != null)
                InterfaceValidator.Validate<IInputable>(_inputableBehaviour);
        }

        private void OnEnable()
        {
            TurnOn();
        }

        private void OnDisable()
        {
            TurnOff();
        }

        public void TurnOn()
        {
            _inputable.Inputting += _moveable.Move;
        }

        public void TurnOff()
        {
            _inputable.Inputting -= _moveable.Move;
        }
    }
}