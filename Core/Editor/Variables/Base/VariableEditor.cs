using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class VariableEditor<V, T, E, EE> : Editor where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        //TODO Dynamically determine these string via member information of the class
        private static readonly string _persistencePropertyPath = "_persistence";
        private static readonly string _defaultValuePropertyPath = "_defaultValue";
        private static readonly string _runtimeValuePropertyPath = "_runtimeValue";

        public static string PersistencePropertyPath => _persistencePropertyPath;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var defaultValueProperty = serializedObject.FindProperty(_defaultValuePropertyPath);
            var runtimeValueProperty = serializedObject.FindProperty(_runtimeValuePropertyPath);
            var useAsConstantProperty = serializedObject.FindProperty(_persistencePropertyPath);

            var isPlaying = Application.isPlaying;

            if (!isPlaying)
                EditorGUILayout.PropertyField(useAsConstantProperty, true);

            if (useAsConstantProperty.boolValue)
                EditorGUILayout.PropertyField(!isPlaying ? defaultValueProperty : runtimeValueProperty,
                    new GUIContent("Constant Value"), true);
            else
            {
                EditorGUI.BeginDisabledGroup(isPlaying);
                EditorGUILayout.PropertyField(defaultValueProperty, new GUIContent("Default Value"), true);
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(!isPlaying);
                EditorGUILayout.PropertyField(runtimeValueProperty, new GUIContent("Runtime Value"), true);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onChangeEvent"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onChangeWithHistoryEvent"), true);

                var v = target as V;
                if (GUILayout.Button("Invoke On Change Event")) v.ForceInvokeOnChangeEvent();

                if (GUILayout.Button("Invoke On Change With History Event")) v.ForceInvokeOnChangeWithHistoryEvent();
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}