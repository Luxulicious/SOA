using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var eso = target as GameEvent;
            if (GUILayout.Button("Invoke"))
            {
                eso.Invoke();
            }
        }
    }

    public abstract class UnityEventSOEditor<ESO, E, T> : Editor where ESO : GameEvent<E, T> where E : UnityEvent<T>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var eso = target as ESO;
            if (GUILayout.Button("Invoke"))
            {
                eso.Invoke(default(T));
            }
        }
    }
}