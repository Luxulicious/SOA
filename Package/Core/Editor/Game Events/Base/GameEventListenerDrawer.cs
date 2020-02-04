using System;
using SOA.Base;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


[CustomPropertyDrawer(typeof(GameEventListener))]
public class GameEventListenerDrawer : PropertyDrawer
{
    //TODO Get field info dynamically
    private static readonly string _responsesPropertyPath = "_responses";
    private static readonly string _gameEventPropertyPath = "_gameEvent";
    private static readonly string _prevGamePropertyPath = "_prevGameEvent";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var responsesProperty = property.FindPropertyRelative(_responsesPropertyPath);
        var gameEventProperty = property.FindPropertyRelative(_gameEventPropertyPath);
        var prevGameEventProperty = property.FindPropertyRelative(_prevGamePropertyPath);

        Object gameEventValue = null;
        try
        {
            gameEventValue = gameEventProperty.objectReferenceValue == null
                ? null
                : gameEventProperty.objectReferenceValue;
        }
        catch (NullReferenceException)
        {
        }

        Object prevGameEventValue = null;
        try
        {
            prevGameEventValue = prevGameEventProperty.objectReferenceValue == null
                ? null
                : prevGameEventProperty.objectReferenceValue;
        }
        catch (NullReferenceException)
        {
        }

        if (property.GetValue() is GameEventListener gameEventListener)
        {
            if (gameEventListener.PrevGameEvent != gameEventValue)
            {
                if (gameEventListener.PrevGameEvent != null)
                {
                    var prevGameEvent = gameEventListener.PrevGameEvent;
                    if (prevGameEvent != null)
                        prevGameEvent.RemoveListener(gameEventListener.InvokeResponses);
                }

                if (gameEventValue != null)
                {
                    var gameEvent = gameEventValue as GameEvent;
                    gameEvent.AddListener(gameEventListener.InvokeResponses);
                }
                gameEventListener.PrevGameEvent = gameEventValue as GameEvent;
            }
        }


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