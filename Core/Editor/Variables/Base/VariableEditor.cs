using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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

        public static Type FindType(string qualifiedTypeName)
        {
            Type t = Type.GetType(qualifiedTypeName);

            if (t != null)
            {
                return t;
            }
            else
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = asm.GetType(qualifiedTypeName);
                    if (t != null)
                        return t;
                }

                return null;
            }
        }

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
                if (runtimeValueProperty.IsPossibleRunTime())
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(defaultValueProperty, true);
                    EditorGUI.EndDisabledGroup();

                    if (isPlaying)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        var runtimeValue = runtimeValueProperty.GetValue();
                        var valueString = runtimeValue?.ToString();
                        var displayValue = runtimeValue != null
                            ? $"{valueString}"
                            : $"None ({typeof(T)})";
                        EditorGUILayout.TextField(runtimeValueProperty.displayName,
                            displayValue);
                        EditorGUI.EndDisabledGroup();

                        if (GUILayout.Button("Go to reference"))
                        {
                            var instance = (runtimeValue as GameObject);
                            if (instance == null) instance = (runtimeValue as Component)?.gameObject;
                            var instanceId = instance.GetInstanceID();
                            Selection.activeInstanceID = instanceId;
                            Selection.activeGameObject = instance;
                        }

                        if (GUILayout.Button("Clear reference"))
                        {
                            runtimeValueProperty.SetValue(null);
                        }

                    }
                    else
                    {
                        if(runtimeValueProperty.GetValue() != null)
                            runtimeValueProperty.SetValue(null);
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.PropertyField(runtimeValueProperty, true);
                        EditorGUI.EndDisabledGroup();
                        
                    }
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField(
                        $"({typeof(T).Name} Variables can only be assigned to at runtime without inspector)");
                    EditorGUI.EndDisabledGroup();
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(isPlaying);
                    EditorGUILayout.PropertyField(defaultValueProperty, true);
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(!isPlaying);
                    EditorGUILayout.PropertyField(runtimeValueProperty, true);
                    EditorGUI.EndDisabledGroup();
                }

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