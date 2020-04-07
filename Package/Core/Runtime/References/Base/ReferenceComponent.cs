using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public class ReferenceComponent<R, V, T, E, EE> : RegisteredMonoBehaviour
        where R : Reference<V, T, E, EE>
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        [SerializeField] protected R _reference;

        public override void Register()
        {
            _reference.Register(this);
        }
    }
}