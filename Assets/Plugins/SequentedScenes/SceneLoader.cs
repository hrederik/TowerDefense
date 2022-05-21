using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.SequentedScenes
{
    [AddComponentMenu("SequentedScenes/Scene Loader")]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private ScenesSequence _sequence;

        private void OnEnable()
        {
            _sequence.Ended += OnSequenceEnded;
        }

        private void OnDisable()
        {
            _sequence.Ended -= OnSequenceEnded;
        }

        public void LoadNext()
        {
            if (_sequence.GetNextScene(out SequenceItem next))
            {
                SceneManager.LoadScene(next.SceneName);
            }
        }

        private void OnSequenceEnded() { }
    }
}
