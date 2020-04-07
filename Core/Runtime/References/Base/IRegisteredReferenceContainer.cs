using UnityEngine;

namespace SOA.Base
{
    /// <summary>
    ///Add this class to where ever you use a reference
    ///and call SetContextObject() in OnAfterDeserialize().
    ///OnBeforeSerialize can be ignored.
    /// </summary>
    public interface IRegisteredReferenceContainer : ISerializationCallbackReceiver
    {
        void Register();
    }
}