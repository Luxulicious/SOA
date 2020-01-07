using System;
using UnityEditor;

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
    }
}