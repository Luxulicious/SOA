	using UnityEngine;

    namespace SOA.Base {
	/// <summary>
    /// A ScriptableObject alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredScriptableObject : ScriptableObject, IRegisteredReferenceContainer
    {
        public virtual void OnAfterDeserialize()
        {
            Register();
        }

        public virtual void OnBeforeSerialize()
        {
            Register();
        }

        public abstract void Register();
    }}