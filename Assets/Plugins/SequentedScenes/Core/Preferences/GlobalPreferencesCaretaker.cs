#if UNITY_EDITOR
using System.IO;
using UnityEngine;

namespace Plugins.SequentedScenes.Core.Preferences
{
    public static class GlobalPreferencesCaretaker
    {
        private const string SaveDirectory = "Assets/Plugins/SequentedScenes/Core/Preferences/Editor/";
        private const string FileName = "Preferences";
        private const string FileExtension = ".json";

        public static void Update()
        {
            if (File.Exists(GetFilePath()))
                Load();
        }

        public static void Reset()
        {
            GlobalPreferences.Reset();

            DeleteSaveFile();
            DeleteMetaFile();
        }

        public static void Save()
        {
            UpdateDirectory();
            var memento = GlobalPreferences.Save();
            var json = JsonUtility.ToJson(memento);
            
            File.WriteAllText(GetFilePath(), json);
        }

        private static void Load()
        {
            var json = File.ReadAllText(GetFilePath());
            var memento = JsonUtility.FromJson<GlobalPreferences.Memento>(json);

            GlobalPreferences.Restore(memento);
        }

        private static void DeleteSaveFile()
        {
            if (File.Exists(GetFilePath()))
                File.Delete(GetFilePath());
        }

        private static void DeleteMetaFile()
        {
            if (File.Exists(GetMetaFilePath()))
                File.Delete(GetMetaFilePath());
        }
        
        private static void UpdateDirectory()
        {
            if (Directory.Exists(SaveDirectory) == false)
                Directory.CreateDirectory(SaveDirectory);
        }
        
        private static string GetFilePath() =>
            $"{SaveDirectory}{FileName}{FileExtension}";

        private static string GetMetaFilePath() =>
            $"{SaveDirectory}{FileName}{FileExtension}{FileExtensions.Meta}";
    }
}
#endif