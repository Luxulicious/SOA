using System;
using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    /// Disables persistence in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisablePersistence : PropertyAttribute
    {
        public DisablePersistence()
        {
        }
    }
}