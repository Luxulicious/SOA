	using UnityEngine;

    namespace SOA.Base {
	/// <summary>
    /// A ScriptableObject alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredScriptableObject : ScriptableObject, IRegisteredReferenceContainer
    {
        public abstract void Register();
    }}