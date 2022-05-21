using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _bar;
    private AsyncOperation _sceneLoading;

    private void Awake()
    {
        var isTutorialCompleted = PlayerPrefs.GetInt("IsTutorialCompleted");

        if (isTutorialCompleted == 0)
        {
            _sceneLoading = SceneManager.LoadSceneAsync("Tutorial");
        }
        else
        {
            _sceneLoading = SceneManager.LoadSceneAsync("Level1");
        }
    }

    private void Update()
    {
        _bar.fillAmount = _sceneLoading.progress;
    }
}