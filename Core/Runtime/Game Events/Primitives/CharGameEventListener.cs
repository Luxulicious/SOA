using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class CharGameEventListener : GameEventListener<CharGameEvent, CharUnityEvent, char>
    {
    }
}