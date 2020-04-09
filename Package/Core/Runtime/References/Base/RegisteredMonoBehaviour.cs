using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    /// A MonoBehaviour alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredMonoBehaviour : MonoBehaviour, IRegisteredReferenceContainer
    {
        public virtual void OnBeforeSerialize()
        {
            Register();
        }

        public virtual void OnAfterDeserialize()
        {
            Register();
        }

        /// <summary>
        /// Set the registration of any reference you want registered here.
        /// </summary>
        public abstract void Register();
    }
}