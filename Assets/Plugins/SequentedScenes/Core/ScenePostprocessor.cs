#if UNITY_EDITOR

using System.IO;
using System.Linq;
using UnityEditor;

namespace Plugins.SequentedScenes.Core
{
    public class ScenePostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            bool ScenePredicate(string assetPath) => Path.GetExtension(assetPath) == FileExtensions.Scene;

            var importedScenes = importedAssets.Where(ScenePredicate).ToArray();
            var deletedScenes = deletedAssets.Where(ScenePredicate).ToArray();

            var movedScenes = movedAssets.Where(ScenePredicate).ToArray();
            var movedFromScenes = movedFromAssetPaths.Where(ScenePredicate).ToArray();

            Process(importedScenes, deletedScenes, movedScenes, movedFromScenes);
        }

        private static void Process(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            ProcessImportedScenes(imported);
            ProcessDeletedScenes(deleted);
            ProcessMovedScenes(moved, movedFrom);
        }

        private static void ProcessImportedScenes(string[] importedScenes)
        {
            foreach (var assetPath in importedScenes)
            {
                if (SceneValidator.IsPathValid(assetPath) == false) continue;
                if (SceneValidator.IsNameValid(Path.GetFileNameWithoutExtension(assetPath)) == false) continue;

                var name = Path.GetFileNameWithoutExtension(assetPath);
                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                
                BuildSettingsFacade.TryAdd(assetPath, guid);
                ScriptableBuilder.TryBuild(name);
            }
        }

        private static void ProcessDeletedScenes(string[] deletedScenes)
        {
            foreach (var assetPath in deletedScenes)
            {
                if (SceneValidator.IsNameValid(Path.GetFileNameWithoutExtension(assetPath)) == false) continue; 
                if (SceneValidator.IsPathValid(assetPath) == false) continue;

                BuildSettingsFacade.Delete(assetPath);
                ScriptableBuilder.Delete(Path.GetFileNameWithoutExtension(assetPath));
            }
        }

        private static void ProcessMovedScenes(string[] moved, string[] movedFrom)
        {
            for (var i = 0; i < movedFrom.Length; i++)
            {
                var oldName = Path.GetFileNameWithoutExtension(movedFrom[i]);
                var newName = Path.GetFileNameWithoutExtension(moved[i]);

                ProcessRenaming(oldName, newName);
                ProcessMoving(movedFrom[i], oldName);
            }
        }

        private static void ProcessMoving(string movedFrom, string oldName)
        {
            if (SceneValidator.IsPathValid(movedFrom) == false || SceneValidator.IsNameValid(oldName)) return;
            ScriptableBuilder.Delete(oldName);
        }

        private static void ProcessRenaming(string oldName, string newName)
        {
            if (oldName != newName && SceneValidator.IsNameValid(oldName))
                ScriptableBuilder.Delete(oldName);
        }
    }
}
#endif