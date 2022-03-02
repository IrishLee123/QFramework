using FrameworkDesign;
using UnityEditor;
using UnityEngine;

namespace CounterApp.Editor
{
    public class EditorCounterApp : EditorWindow, IController
    {
        [MenuItem("FrameDemo/CounterApp")]
        private static void ShowWindow()
        {
            CounterApp.OnRegisterPatch += app => { app.RegisterUtility<IStorage>(new EditorPrefsStorage()); };

            var window = GetWindow<EditorCounterApp>();
            window.position = new Rect(100, 100, 400, 600);
            window.titleContent = new GUIContent("CounterApp");
            window.Show();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Interface;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                this.SendCommand<AddCountCommand>();
            }

            GUILayout.Label(this.GetModel<ICounterModel>().Count.Value.ToString());

            if (GUILayout.Button("-"))
            {
                this.SendCommand<SubCountCommand>();
            }
        }
    }
}