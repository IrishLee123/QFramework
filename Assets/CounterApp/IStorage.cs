using FrameworkDesign;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace CounterApp
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int defaultValue = 0);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }

    public class EditorPrefsStorage : IStorage
    {
        public int LoadInt(string key, int defaultValue = 0) =>
#if UNITY_EDITOR
            EditorPrefs.GetInt(key, defaultValue);
#else
            return 0;
#endif


        public void SaveInt(string key, int value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt(key, value);
#endif
        }
    }
}
