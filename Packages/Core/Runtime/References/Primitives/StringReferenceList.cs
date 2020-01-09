using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class StringReferenceList : ReferenceList<StringReference, StringVariable, string, StringUnityEvent, StringStringUnityEvent
        , StringReferenceUnityEvent>
    {
    }
}