using UnityEngine;
using UnityEngine.Events;

namespace Game.Tutorial
{
    public class Tutor : MonoBehaviour
    {
        [SerializeField] private UnityEvent _started;
        [SerializeField] private UnityEvent[] _steps;
        [SerializeField] private UnityEvent _stopped;
        private int _index;

        private void Start()
        {
            _started?.Invoke();
        }

        public void TryToNextStep()
        {
            if (_index < _steps.Length)
            {
                NextStep();
            }
            else
            {
                _stopped?.Invoke();

                PlayerPrefs.SetInt("IsTutorialCompleted", 1);
            }
        }

        private void NextStep()
        {
            _steps[_index]?.Invoke();
            _index++;
        }
    }
}