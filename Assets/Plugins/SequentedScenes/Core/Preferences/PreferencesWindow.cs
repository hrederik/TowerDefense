#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Plugins.SequentedScenes.Core.Preferences
{
    public class PreferencesWindow : EditorWindow
    {
        private string _checkableDirectory;
        private string _savingDirectory;
        private bool _addNewSceneToBuildList;
        private List<string> _keywords;
        
        private bool _keywordsFoldout;
        private ReorderableList _keywordsList;
        
        [MenuItem("Window/SequentedScenes", priority = 99)]
        private static void ShowWindow()
        {
            var window = GetWindow<PreferencesWindow>("SequentedScenes");
            window.Show();
        }

        private void Awake()
        {
            GlobalPreferencesCaretaker.Update();
            _keywords = new List<string>();
            FillFields();
        }

        private void OnEnable()
        {
            _keywordsList = new ReorderableList(_keywords, typeof(string), true, true, true, true);
        }

        private void OnGUI()
        {
            GUILayout.Space(15);
            DrawFields();
            GUILayout.Space(15);
            DrawList();
            GUILayout.Space(15);
            DrawButtons();
        }

        private void DrawFields()
        {
            GUILayout.Label("Checkable directory", EditorStyles.boldLabel);
            _checkableDirectory = GUILayout.TextField(_checkableDirectory);
            
            GUILayout.Space(5);
            
            GUILayout.Label("Saving directory", EditorStyles.boldLabel);
            _savingDirectory = GUILayout.TextField(_savingDirectory);
            
            GUILayout.Space(5);
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Add new scene to build list", EditorStyles.boldLabel, GUILayout.Width(175));
            _addNewSceneToBuildList = GUILayout.Toggle(_addNewSceneToBuildList, "");
            
            GUILayout.EndHorizontal();
        }

        private void DrawList()
        {
            _keywordsFoldout = EditorGUILayout.Foldout(_keywordsFoldout, "Keywords", true);

            if (_keywordsFoldout == false) return;

            GUILayout.Space(5);

            _keywordsList.DoLayoutList();

            _keywordsList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Keywords");
            };
            
            _keywordsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (index >= _keywordsList.list.Count) return;
                var element = _keywordsList.list[index];

                _keywords[index] = EditorGUI.TextField(
                    new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    (string) element);
            };

            _keywordsList.onAddCallback = (list) =>
            {
                _keywords.Add("");
            };
        }

        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Apply", GUILayout.Height(25))) 
                Apply();

            GUILayout.Space(5);
            
            if (GUILayout.Button("Reset", GUILayout.Height(25))) 
                ResetToDefault();
            
            GUILayout.EndHorizontal();
        }

        private void Apply()
        {
            GlobalPreferences.CheckableDirectory = _checkableDirectory;
            GlobalPreferences.SavingDirectory = _savingDirectory;
            GlobalPreferences.AddNewSceneToBuildList = _addNewSceneToBuildList;
            GlobalPreferences.Keywords = _keywords.ToArray();
            
            GlobalPreferencesCaretaker.Save();
        }

        private void ResetToDefault()
        {
            GlobalPreferencesCaretaker.Reset();
            FillFields();
        }

        private void FillFields()
        {
            _checkableDirectory = GlobalPreferences.CheckableDirectory;
            _savingDirectory = GlobalPreferences.SavingDirectory;
            _addNewSceneToBuildList = GlobalPreferences.AddNewSceneToBuildList;
            
            
            _keywords.Clear();
            _keywords.AddRange(GlobalPreferences.Keywords);
            // _keywords = GlobalPreferences.Keywords.ToList();
        }
    }
}
#endif