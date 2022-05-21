using UnityEngine;
using UnityEngine.UI;
using Game.Bots.Emit;

namespace UI.Ingame
{
    public class ProgressIndicator : MonoBehaviour
    {
        [SerializeField] private Text _label;
        [SerializeField] private Image[] _bars;
        [SerializeField] private Wave[] _waves;
        private int _currentIndex;

        private void OnEnable()
        {
            InitializeWave(_waves[0]);
        }

        private void InitializeWave(Wave wave)
        {
            _waves[0].BotKilled -= Refresh;
            _waves[0].AllBotsKilled -= OnWaveEnded;

            wave.BotKilled += Refresh;
            wave.AllBotsKilled += OnWaveEnded;

            _label.text = $"WAVE {_currentIndex + 1}";
        }

        private void Refresh()
        {
            var progress = (float)_waves[_currentIndex].KiledBotsAmount / _waves[_currentIndex].Bots.Count;

            _bars[_currentIndex].fillAmount = progress;
        }

        private void OnWaveEnded()
        {
            _currentIndex++;

            if (_currentIndex < _waves.Length)
            {
                InitializeWave(_waves[_currentIndex]);
            }
        }
    }
}