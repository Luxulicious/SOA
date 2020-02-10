using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SOA.Base
{
    public static class SerializedPropertyExtensions
    {
        private static string _displayNameIdentifier = "Element";

        //TODO This method has not been properly tested, and is fallible
        /// <summary>
        /// Checks if a given property is part of a serialized collection visible in the unity inspector
        /// </summary>
        /// <param name="property">Property to evaluate</param>
        /// <returns></returns>
        [Obsolete("Method may be fallible, use at own risk")]
        public static bool IsPartOfArray(this SerializedProperty property)
        {
            return property.displayName.Contains(_displayNameIdentifier);
        }

        public static float FindPropertyRelativeAndGetHeight(this SerializedProperty property, string propertyPath)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(propertyPath));
        }

        public static object GetValue(this SerializedProperty property)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetField(property.propertyPath);
            if (fi == null)
                fi = parentType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            return fi.GetValue(property.serializedObject.targetObject);
        }

        public static void SetValue(this SerializedProperty property, object value)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetField(property.propertyPath);
            if (fi == null)
                fi = parentType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(property.serializedObject.targetObject, value);
        }

        public static bool IsPartOfAnyScene(this SerializedProperty property)
        {
            var value = GetValue(property);
            if (value == null)
                return false;
            var gameObject = value as GameObject;
            if (gameObject)
                return gameObject.scene.rootCount != 0;
            var component = value as Component;
            if (component)
                return component.gameObject.scene.rootCount != 0;
            return false;
        }
    }
}