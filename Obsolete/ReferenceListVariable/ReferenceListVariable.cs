using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class ReferenceListVariable<RL, R, V, T, E, EE, RE> : ScriptableObject
        where RL : ReferenceList<R, V, T, E, EE, RE>, new()
        where R : Reference<V, T, E, EE>, new()
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
        where RE : UnityEvent<R>, new()
    {
        //Uses a reference list instead
        [SerializeField] protected RL _items = new RL();
    }
}