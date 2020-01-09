using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorReferenceList : ReferenceList<ColorReference, ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent
        , ColorReferenceUnityEvent>
    {
    }
}