using UnityEngine;
using UnityEngine.Events;

namespace CustomUserInput.ConcreteInputs
{
    public class Keyboard : MonoBehaviour, IInputable
    {
        public event UnityAction<Vector3> Inputting;
        public event UnityAction Started;
        public event UnityAction Stopped;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            var newVector = new Vector3(horizontal, 0, vertical);

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                Started?.Invoke();
            }

            Inputting?.Invoke(newVector);

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                Stopped?.Invoke();
            }
        }
    }
}