using UnityEngine;
using UnityEngine.UI;

namespace Game.Stats
{
    public class HealthTextPresenter : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Text _label;

        private void OnEnable()
        {
            _health.Changed += Refresh;
        }

        private void OnDisable()
        {
            _health.Changed -= Refresh;
        }

        private void Refresh()
        {
            _label.text = _health.Current.ToString();
        }
    }
}