using System;
using SOA.Base;
using System;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class CharReferenceList : ReferenceList<CharReference, CharVariable, char, CharUnityEvent, CharCharUnityEvent
        , CharReferenceUnityEvent>
    {
    }
}