#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Plugins.SequentedScenes.Core.Preferences
{
    public static class GlobalPreferences
    {
        private const string DefaultCheckableDirectory = "Assets/Scenes/";
        private const string DefaultSavingDirectory = "Assets/Scenes/Sequence/Items/";
        private const bool DefaultAddNewSceneToBuildList = true;
        private static readonly string[] DefaultKeywords = { };

        public static string CheckableDirectory { get; set; } = DefaultCheckableDirectory;
        public static string SavingDirectory { get; set; } = DefaultSavingDirectory;
        public static bool AddNewSceneToBuildList { get; set; } = DefaultAddNewSceneToBuildList;
        public static string[] Keywords { get; set; } = DefaultKeywords;

        public static Memento Save() => 
            new Memento(
                CheckableDirectory, 
                SavingDirectory, 
                AddNewSceneToBuildList, 
                Keywords);

        public static void Restore(Memento memento)
        {
            CheckableDirectory = memento.SavedCheckableDirectory;
            SavingDirectory = memento.SavedSavingDirectory;
            AddNewSceneToBuildList = memento.SavedAddNewSceneToBuildList;
            Keywords = memento.SavedKeywords;
        }

        public static void Reset()
        {
            SavingDirectory = DefaultSavingDirectory;
            CheckableDirectory = DefaultCheckableDirectory;
            AddNewSceneToBuildList = DefaultAddNewSceneToBuildList;
            Keywords = DefaultKeywords;
        }

        [Serializable]
        public class Memento
        {
            [SerializeField] private string _savedCheckableDirectory;
            [SerializeField] private string _savedSavingDirectory;
            [SerializeField] private bool _savedAddNewSceneToBuildList;
            [SerializeField] private string[] _savedKeywords;
            
            public Memento(string savedCheckableSavedCheckableDirectory, string savingDirectory, bool addNewSceneToBuildList, string[] keywords)
            {
                _savedCheckableDirectory = savedCheckableSavedCheckableDirectory;
                _savedSavingDirectory = savingDirectory;
                _savedAddNewSceneToBuildList = addNewSceneToBuildList;
                _savedKeywords = keywords;
            }

            public string SavedCheckableDirectory => _savedCheckableDirectory;
            public string SavedSavingDirectory => _savedSavingDirectory;
            public bool SavedAddNewSceneToBuildList => _savedAddNewSceneToBuildList;
            public string[] SavedKeywords => _savedKeywords;
        }
    }
}
#endif