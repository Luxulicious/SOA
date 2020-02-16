using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Color List", menuName = "SOA/UnityEngine/Color/List", order = 1)]
    public class ColorReferenceListVariable : ReferenceListVariable<ColorReferenceList, ColorReference, ColorVariable, Color,
        ColorUnityEvent, ColorColorUnityEvent, ColorReferenceUnityEvent>
    {
    }
}