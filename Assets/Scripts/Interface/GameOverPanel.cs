using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI.Ingame
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private GameObject _loseWindow;

        public event UnityAction ReloadButtonClicked;

        public void OnReloadButtonClick()
        {
            ReloadButtonClicked?.Invoke();

            //var currentScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(currentScene.name);
        }

        public void OnGameOver()
        {
            _loseWindow.SetActive(true);
            OnGameEnd();
        }

        public void OnGameWin()
        {
            _winWindow.SetActive(true);
            OnGameEnd();
        }

        private void OnGameEnd()
        {
            _endGamePanel.SetActive(true);
        }
    }
}