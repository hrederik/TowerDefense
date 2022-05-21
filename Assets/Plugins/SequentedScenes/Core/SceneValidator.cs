using System.Linq;
using Plugins.SequentedScenes.Core.Preferences;

#if UNITY_EDITOR

namespace Plugins.SequentedScenes.Core
{
    public static class SceneValidator
    {
        public static bool IsPathValid(string path)
        {
            var lastIndex = path.LastIndexOf('/');
            var pathWithoutFile = path.Substring(0, lastIndex + 1);

            return string.Equals(pathWithoutFile, GlobalPreferences.CheckableDirectory);
        }

        public static bool IsNameValid(string sceneName)
        {
            if (GlobalPreferences.Keywords.Length > 0)
                return GlobalPreferences.Keywords.Any(sceneName.Contains);
            else 
                return true;
        }
    }
}

#endif