using UnityEngine.Events;

namespace SOA.Base
{
    public interface IRegisteredReference
    {
        void Register(IRegisteredReferenceContainer context);
        bool HasRegistration();
        void Ping();
        void Select();
    }
    
    public interface IRegisteredReference<V, T, E, EE> : IRegisteredReference/*<T, E, EE>*/ where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        void Ping(V variable);
        void Select(V variable);
    }
}