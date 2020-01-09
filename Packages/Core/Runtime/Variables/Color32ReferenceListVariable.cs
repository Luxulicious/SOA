using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Color32 List", menuName = "SOA/UnityEngine/Color32/List", order = 1)]
    public class Color32ReferenceListVariable : ReferenceListVariable<Color32ReferenceList, Color32Reference, Color32Variable, Color32,
        Color32UnityEvent, Color32Color32UnityEvent, Color32ReferenceUnityEvent>
    {
    }
}