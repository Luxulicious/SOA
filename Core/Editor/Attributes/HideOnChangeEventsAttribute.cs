using System;
using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    /// Hides on value changed events from the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HideOnValueChangedEventsAttribute : PropertyAttribute
    {
        public HideOnValueChangedEventsAttribute()
        {
        }
    }
}