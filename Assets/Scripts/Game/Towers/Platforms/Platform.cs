using UnityEngine;
using UnityEngine.Events;

namespace Game.Towers
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _graphics;
        [SerializeField] private Blinking _blinking;
        [SerializeField] private UnityEvent _towerBuilt;

        public Vector3 Position { get; private set; }

        private void Awake()
        {
            Position = transform.position;

            WakeUp();
        }

        public void WakeUp()
        {
            _blinking.Launch();
            _graphics.SetActive(true);
            _collider.enabled = true;
        }

        public void Sleep()
        {
            _towerBuilt?.Invoke();
            _blinking.Stop();

            _graphics.SetActive(false);
            _collider.enabled = false;
        }
    }
}