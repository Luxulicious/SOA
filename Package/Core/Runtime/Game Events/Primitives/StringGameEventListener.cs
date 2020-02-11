using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringGameEventListener : GameEventListener<StringGameEvent, StringUnityEvent, string>
    {
    }
}