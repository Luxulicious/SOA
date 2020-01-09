using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringReference : Reference<StringVariable, string, StringUnityEvent, StringStringUnityEvent>
    {
    }
}