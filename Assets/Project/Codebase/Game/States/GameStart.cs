using UnityEngine;
using UnityEngine.Events;
using Game.Bots.Emit;

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
        }
    }
}