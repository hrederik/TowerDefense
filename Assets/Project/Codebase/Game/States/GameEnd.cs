using UnityEngine;
using UnityEngine.SceneManagement;
using UI.Ingame;
using Game.Bots.Emit;
using Game.Movement;
using CustomUserInput;

namespace Game.States
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private string _levelName;
        [SerializeField] private string _nextScene;
        [SerializeField] private Stopwatch _stopwatch;
        [SerializeField] private ProgressCalculator _progressCalculator;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private UserInput _userInput;
        [SerializeField] private MonoBehaviour _moveableBehaviour;
        [SerializeField] private BotsContainer _botsContainer;
        private bool _isGameEnded;

        private IMoveable _moveable => (IMoveable)_moveableBehaviour;

        private void OnValidate()
        {
            if (_moveableBehaviour != null)
                InterfaceValidator.Validate<IMoveable>(_moveableBehaviour);
        }

        private void OnEnable()
        {
            _gameOverPanel.ReloadButtonClicked += LoadNextScene;
        }

        private void OnDisable()
        {
            _gameOverPanel.ReloadButtonClicked -= LoadNextScene;
        }

        public void OnGameOver()
        {
            if (_isGameEnded == false)
            {
                _gameOverPanel.OnGameOver();
                OnGameEnd();

                _isGameEnded = true;
            }
        }

        public void OnGameWin()
        {
            if (_isGameEnded == false)
            {
                _gameOverPanel.OnGameWin();
                OnGameEnd();

                _isGameEnded = true;
            }
        }

        private void OnGameEnd()
        {
            _moveable.Move(Vector3.zero);
            _botsContainer.SleepAllBots();

            _stopwatch.Pause();
            _userInput.TurnOff();
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(_nextScene);
        }
    }
}