using UnityEngine;
using UnityEngine.Events;
using Game.Bots.Emit;
using Monetization.Metrica;

namespace Game.States
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private string _levelName;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Stopwatch _stopwatch;
        [SerializeField] private UnityEvent _started;

        public void OnGameStart()
        {
            _tutorialPanel.SetActive(false);
            _spawner.Launch();
            _started?.Invoke();

            _stopwatch.Launch();
            SendEvent();
        }

        private void SendEvent()
        {
            var levelCount = PlayerPrefs.GetInt("AmountGames");
            var ingameMetrica = MetricaFacadesHub.Instance.Ingame;

            ingameMetrica.SendLevelStart(_levelNumber, _levelName, ++levelCount);
            PlayerPrefs.SetInt("AmountGames", levelCount);
        }
    }
}