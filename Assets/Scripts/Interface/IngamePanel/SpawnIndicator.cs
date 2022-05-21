using UnityEngine;
using UnityEngine.UI;

namespace UI.Ingame
{
    public class SpawnIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject _indicator;
        [SerializeField] private Image _bar;
        [SerializeField] private Countdown _countdown;

        private void OnEnable()
        {
            _countdown.Started += Show;
            _countdown.Ticked += Refresh;
            _countdown.Ended += Hide;
        }

        private void OnDisable()
        {
            _countdown.Started -= Show;
            _countdown.Ticked -= Refresh;
            _countdown.Ended -= Hide;
        }

        private void Show()
        {
            _indicator.SetActive(true);
        }

        private void Refresh()
        {
            _bar.fillAmount = 1 - _countdown.NormalizedValue;
        }

        private void Hide()
        {
            _indicator.SetActive(false);
            _bar.fillAmount = 1;
        }
    }
}