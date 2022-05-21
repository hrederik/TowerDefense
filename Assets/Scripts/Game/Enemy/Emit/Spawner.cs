using UnityEngine;
using System.Collections;

namespace Game.Bots.Emit
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _botDelay;
        [SerializeField] private Countdown _countdown;
        [SerializeField] private Transform _defaultPosition;
        [SerializeField] private Wave[] _waves;
        private int _currentWaveIndex;

        private void OnEnable()
        {
            _countdown.Ended += LaunchWave;
            _waves[0].AllBotsKilled += OnWaveEnded;
        }

        private void OnDisable()
        {
            _countdown.Ended -= LaunchWave;
            _waves[0].AllBotsKilled -= OnWaveEnded;
        }

        public void Launch()
        {
            _countdown.Launch();
        }

        public void TryToSkip()
        {
            if (_countdown.IsLaunched)
            {
                _countdown.Stop();
            }
        }

        private void LaunchWave()
        {
            if (_currentWaveIndex < _waves.Length)
            {
                var currentWave = _waves[_currentWaveIndex];
                currentWave.Launch();

                ResetAllBots(currentWave);
                StartCoroutine(Spawn(currentWave));
            }
        }

        private void OnWaveEnded()
        {
            _countdown.Launch();
        }

        private void ResetAllBots(Wave wave)
        {
            foreach (var bot in wave.Bots)
            {
                bot.Clear();
                bot.transform.position = _defaultPosition.position;
            }
        }

        private IEnumerator Spawn(Wave wave)
        {
            foreach (var bot in wave.Bots)
            {
                yield return new WaitForSeconds(_botDelay);

                bot.gameObject.SetActive(true);
                bot.OnLaunch();
            }

            _currentWaveIndex++;
        }
    }
}