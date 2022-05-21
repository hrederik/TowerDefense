using UnityEngine;
using UnityEngine.Events;

namespace Game.Stats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _max;
        [SerializeField] private UnityEvent _ended;
        private int _current;

        public event UnityAction Changed;
        public event UnityAction Ended
        {
            add => _ended.AddListener(value);
            remove => _ended.RemoveListener(value);
        }

        public int Max => _max;
        public int Current => _current;

        private void Start()
        {
            Clear();
        }

        public void Increase(int amount)
        {
            _current += amount;

            if (_current > _max)
            {
                _current = _max;
            }

            Changed?.Invoke();
        }

        public void Decrease(int amount)
        {
            if (_current > 0)
            {
                _current -= amount;
                Changed?.Invoke();
                CheckForEmpty();
            }
        }

        public void Clear()
        {
            _current = _max;
            Changed?.Invoke();
        }


        private void CheckForEmpty()
        {
            if (_current <= 0)
            {
                _ended?.Invoke();
            }
        }
    }
}