using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class CharGameEventListener : GameEventListener<CharGameEvent, CharUnityEvent, char>
    {
    }
}