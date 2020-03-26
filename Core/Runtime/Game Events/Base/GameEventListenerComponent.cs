using SOA.Base;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Common.Primitives
{
    public class GameEventListenerComponent : MonoBehaviour
    {
        [SerializeField] private GameEventListener _listener;
    }

    public class GameEventListenerComponent<GEL, GE, E, T> : MonoBehaviour where GEL : GameEventListener<GE, E, T>
        where GE : GameEvent<E, T>, new()
        where E : UnityEvent<T>, new()
    {
        [SerializeField] protected GEL _listener;
    }
}