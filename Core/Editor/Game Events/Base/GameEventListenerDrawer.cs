using SOA.Base;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(GameEventListener))]
public class GameEventListenerDrawer : PropertyDrawer
{
    //TODO Get field info dynamically
    private static readonly string _responsesPropertyPath = "_responses";
    private static readonly string _gameEventPropertyPath = "_gameEvent";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var responsesProperty = property.FindPropertyRelative(_responsesPropertyPath);
        var gameEventProperty = property.FindPropertyRelative(_gameEventPropertyPath);

        label = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, label);
        var contentRect = EditorGUI.PrefixLabel(position, label);
        var gameEventRect = new Rect(contentRect.x, contentRect.y, contentRect.width,
            EditorGUI.GetPropertyHeight(gameEventProperty));
        EditorGUI.PropertyField(gameEventRect, gameEventProperty, GUIContent.none, true);
        EditorGUI.PropertyField(
            new Rect(gameEventRect.x, gameEventRect.y + gameEventRect.height + 2f, contentRect.width,
                EditorGUI.GetPropertyHeight(responsesProperty)), responsesProperty, true);
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var responsesProperty = property.FindPropertyRelative(_responsesPropertyPath);
        var gameEventProperty = property.FindPropertyRelative(_gameEventPropertyPath);
        var responsesPropertyHeight = EditorGUI.GetPropertyHeight(responsesProperty);
        var gameEventPropertyHeight = EditorGUI.GetPropertyHeight(gameEventProperty);
        var height = responsesPropertyHeight + gameEventPropertyHeight + 2f;
        return height;
    }
}