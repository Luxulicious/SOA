using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomEditor(typeof(GameObjectGameEvent))]
    public class GameObjectGameEventEditor : GameEventEditor<GameObjectGameEvent, GameObjectUnityEvent, GameObject>
    {
    }
}