using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringGameEventListener : GameEventListener<StringGameEvent, StringUnityEvent, string>
    {
    }
}