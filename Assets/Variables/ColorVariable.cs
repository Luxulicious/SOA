using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Color Variable", menuName = "SOA/UnityEngine/Color/Variable", order = 1)]
    public class ColorVariable : Variable<Color, ColorUnityEvent, ColorColorUnityEvent>
    {
    }
}