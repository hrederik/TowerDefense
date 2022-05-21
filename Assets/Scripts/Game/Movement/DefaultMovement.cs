using UnityEngine;
using UnityEngine.Events;

namespace Game.Movement
{
    public class DefaultMovement : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private Transform _camera;
        [SerializeField] private CharacterController _characterController;
        private float _turnSmoothVelocity;
        private Transform _transform;
        private Vector3 _velocity;

        public event UnityAction MovementStarted;
        public event UnityAction MovementStopped;

        public float Speed { get; set; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            Speed = _speed;
        }

        private void Update()
        {
            if (_velocity.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(_velocity.x, _velocity.z) * Mathf.Rad2Deg;// + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

                _transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                if (_characterController.isGrounded == false)
                {
                    moveDir.y += -9.81f;
                }

                _characterController.Move(moveDir * Speed * Time.deltaTime);
                MovementStarted?.Invoke();
            }
            else
            {
                MovementStopped?.Invoke();
            }
        }

        public void Move(Vector3 direction)
        {
            _velocity = direction;
        }

        public void ResetSpeed()
        {
            Speed = _speed;
        }
    }
}