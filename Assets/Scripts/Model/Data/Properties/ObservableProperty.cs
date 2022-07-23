using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Model.Data.Properties
{
    public class ObservableProperty : ScriptableObject
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}