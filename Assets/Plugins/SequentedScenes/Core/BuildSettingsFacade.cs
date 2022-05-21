#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using Plugins.SequentedScenes.Core.Preferences;
using UnityEditor;

namespace Plugins.SequentedScenes.Core
{
    public static class BuildSettingsFacade
    {
        public static void TryAdd(string path, string guid)
        {
            var cannotAddNewScene = ContainsScene(guid) || GlobalPreferences.AddNewSceneToBuildList == false; 
            if (cannotAddNewScene) return;
            
            var newSceneList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes)
            {
                new EditorBuildSettingsScene(path, true)
            };

            EditorBuildSettings.scenes = newSceneList.ToArray();
        }

        public static void Delete(string path)
        {
            var newSceneList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            newSceneList.RemoveAll(scene => scene.path == path);

            EditorBuildSettings.scenes = newSceneList.ToArray();
        }

        private static bool ContainsScene(string guid)
        {
            return EditorBuildSettings.scenes.Any(scene => scene.guid.ToString() == guid);
        }
    }
}

#endif