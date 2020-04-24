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

        private void OnEnable()
        {
            Register();
        }

        private void OnDisable()
        {
            UnRegister(); 
        }
        
        public abstract void Register();

        public abstract void UnRegister();
    }}