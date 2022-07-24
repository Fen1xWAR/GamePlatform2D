﻿using UnityEditor;
using UnityEngine;
using UnityEditor.UI;

namespace Scripts
{
    [CustomEditor(typeof(CustomButton), true)]
    [CanEditMultipleObjects]
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_normal"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_pressed"));
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}