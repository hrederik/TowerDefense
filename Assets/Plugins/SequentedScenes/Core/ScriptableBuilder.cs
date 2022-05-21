#if UNITY_EDITOR

using System;
using System.IO;
using System.Reflection;
using Plugins.SequentedScenes.Core.Preferences;
using UnityEditor;
using UnityEngine;

namespace Plugins.SequentedScenes.Core
{
    public static class ScriptableBuilder
    {
        public static void TryBuild(string classname)
        {
            var path = GlobalPreferences.SavingDirectory + classname + FileExtensions.Asset;

            if (File.Exists(path) == false) 
                Build(classname, path);
        }

        public static void Delete(string className)
        {
            var path = GlobalPreferences.SavingDirectory + className + FileExtensions.Asset;

            if (File.Exists(path))
            {
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }

        private static void Build(string name, string path)
        {
            var instance = ScriptableObject.CreateInstance<SequenceItem>();
            
            UpdateDirectory();
            FillInstanceField(name, instance);

            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();
        }

        private static void FillInstanceField(string name, SequenceItem instance)
        {
            var fieldInfo = typeof(SequenceItem).GetField("_sceneName", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo == null)
                throw new NotImplementedException();

            fieldInfo.SetValue(instance, name);
        }

        private static void UpdateDirectory()
        {
            if (Directory.Exists(GlobalPreferences.SavingDirectory) == false)
                Directory.CreateDirectory(GlobalPreferences.SavingDirectory);
        }
    }
}

#endif