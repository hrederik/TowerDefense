using UnityEngine;

namespace Plugins.SequentedScenes
{
    public class SequenceItem : ScriptableObject
    {
        [HideInInspector, SerializeField] private string _sceneName;

        public string SceneName => _sceneName;
    }
}