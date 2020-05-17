using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    ///Add this class to where ever you use a reference
    /// </summary>
    public interface IRegisteredReferenceContainer
    {
        /// <summary>
        /// Set the registration of any reference you want registered here.
        /// </summary>
        void Register();
    }
}