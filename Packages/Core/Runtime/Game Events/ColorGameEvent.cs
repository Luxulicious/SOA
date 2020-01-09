using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Color Event", menuName = "SOA/UnityEngine/Color/Event", order = 1)]
    public class ColorGameEvent : GameEvent<ColorUnityEvent, Color>
    {
    }
}