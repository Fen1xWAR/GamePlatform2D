using UnityEditor;
using UnityEngine;
using System;

namespace Scripts
{
    [CustomEditor(typeof(ShowDialogComponent))]
    public class ShowDialogComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _modeProperty;
        private void OnEnable()
        {
           _modeProperty = serializedObject.FindProperty("_mode");
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_modeProperty);
            ShowDialogComponent.Mode mode;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("externalData"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Tag"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Checkpoint"));

            if (_modeProperty.GetEnum(out mode))
            {
                switch (mode)
                {
                    case ShowDialogComponent.Mode.Bound:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_bound"));
                        break;
                    case ShowDialogComponent.Mode.External:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("external"));
                        break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}