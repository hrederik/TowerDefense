using UnityEngine;
using UnityEngine.UI;

namespace Game.Stats
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _presenter;
        [SerializeField] private Health _health;
        [SerializeField] private Image _bar;

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
            _presenter.SetActive(_health.Current != _health.Max);
            _bar.fillAmount = (float)_health.Current / _health.Max;
        }
    }
}