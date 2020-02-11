using System.Xml.Schema;
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
            {
                EditorGUILayout.PropertyField(!isPlaying ? defaultValueProperty : runtimeValueProperty,
                    new GUIContent("Constant Value"), true);
            }
            else
            {
                EditorGUI.BeginDisabledGroup(isPlaying);
                EditorGUILayout.PropertyField(defaultValueProperty, new GUIContent("Default Value"), true);
                EditorGUI.EndDisabledGroup();
                if (runtimeValueProperty.IsPartOfAnyScene())
                {
                    Debug.Log("Scene object detected!");
                    if (!isPlaying)
                    {
                        runtimeValueProperty.SetValue(null);
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.PropertyField(runtimeValueProperty, new GUIContent("Runtime Value"), true);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.TextField(runtimeValueProperty?.displayName, runtimeValueProperty.GetValue()?.ToString());
                        EditorGUI.EndDisabledGroup();
                        if (GUILayout.Button("Go to reference"))
                        {
                            var instance = (runtimeValueProperty.GetValue() as GameObject);
                            if (!instance) instance = (runtimeValueProperty.GetValue() as Component)?.gameObject;
                            var instanceId = instance.GetInstanceID();
                            Selection.activeInstanceID = instanceId;
                            Selection.activeGameObject = instance;
                        }
                    }
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(!isPlaying);
                    EditorGUILayout.PropertyField(runtimeValueProperty, new GUIContent("Runtime Value"), true);
                    EditorGUI.EndDisabledGroup();
                }

                var isRunTimeOnlyType = typeof(T) == typeof(GameObject) || typeof(T) == typeof(Component);
                if (isRunTimeOnlyType && !isPlaying) EditorGUILayout.LabelField($"({typeof(T).Name} Variables can only be assigned to at runtime without inspector)");


                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onChangeEvent"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onChangeWithHistoryEvent"), true);

                EditorGUI.BeginDisabledGroup(!isPlaying);
                var v = target as V;
                if (GUILayout.Button("Invoke On Change Event")) v.ForceInvokeOnChangeEvent();
                if (GUILayout.Button("Invoke On Change With History Event")) v.ForceInvokeOnChangeWithHistoryEvent();
                EditorGUI.EndDisabledGroup();
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}