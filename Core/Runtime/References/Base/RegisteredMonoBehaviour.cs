using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    /// A MonoBehaviour alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredMonoBehaviour : MonoBehaviour, IRegisteredReferenceContainer
    {
        public abstract void Register();
    }
}