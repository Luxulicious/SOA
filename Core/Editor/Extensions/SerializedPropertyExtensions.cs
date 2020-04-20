using System;
using System.Linq;
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

        public static float GetRelativePropertyHeight(this SerializedProperty property, string propertyPath)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(propertyPath));
        }

        public static object GetValue(this SerializedProperty property)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetFieldViaPath(property.propertyPath);
            return fi?.GetValue(property.serializedObject.targetObject);
        }

        public static T GetValue<T>(this SerializedProperty property)
        {
            return (T) property.GetValue();
        }

        public static void SetValue(this SerializedProperty property, object value)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetFieldViaPath(property.propertyPath);
            fi?.SetValue(property.serializedObject.targetObject, value);
        }


        public static Type GetType(SerializedProperty property)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetFieldViaPath(property.propertyPath);
            return fi?.FieldType;
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

        public static bool IsPossibleRunTime(this SerializedProperty property)
        {
            var type = GetType(property);
            if (type != null)
                return type.IsSubclassOf(typeof(GameObject)) || type == typeof(GameObject) ||
                       type.IsSubclassOf(typeof(Component)) || type == typeof(Component);
            throw new NullReferenceException($"Failed to get type of {property}");
        }

        /// <summary>
        /// Checks if SerializedProperty has attribute of type T
        /// </summary>
        /// <typeparam name="T">Type of attribute</typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this SerializedProperty property)
        {
            var parentType = property.serializedObject.targetObject.GetType();
            var fieldInfo = parentType.GetField(property.name);
            var attributes = fieldInfo.GetCustomAttributes();
            var hasAttribute = attributes.Any(x => x is /*TODO Use T in refactor*/ T);
            return hasAttribute;
        }

        public static FieldInfo GetFieldViaPath(this Type type, string path)
        {
            var parentType = type;
            var fi = type.GetField(path);
            var perDot = path.Split('.');
            foreach (var fieldName in perDot)
            {
                fi = parentType.GetField(fieldName);
                if (fi == null)
                    fi = parentType.GetField(path, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi != null)
                    parentType = fi.FieldType;
                else
                    return null;
            }
            if (fi != null)
                return fi;
            else return null;
        }
    }
}