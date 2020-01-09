using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class ReferenceList<R, V, T, E, EE, RE> : IList<R> where R : Reference<V, T, E, EE>, new()
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
        where RE : UnityEvent<R>, new()
    {
        public ReferenceList()
        {
            _items = new List<R>();
        }

        [SerializeField] protected List<R> _items;
        [SerializeField] protected RE _onAddedEvent = new RE();
        [SerializeField] protected RE _onRemovedEvent = new RE();

        [SerializeField] [HideInInspector] protected bool _eventsFoldedOut = false;

        public virtual IEnumerator<R> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public virtual void AddLocal(T value, bool isConstant)
        {
            var item = new R()
            {
                LocalValue = value, Persistence = isConstant ? Persistence.Constant : Persistence.Variable,
                Scope = Scope.Local
            };
            _items.Add(item);
        }

        public virtual void AddVariable(V variable)
        {
            var item = new R()
            {
                GlobalValue = variable, Persistence = variable.Persistence, Scope = Scope.Global
            };
            _items.Add(item);
        }

        public void Add(R item)
        {
            _items.Add(item);
            _onAddedEvent.Invoke(item);
        }


        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(R item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(R[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(R item)
        {
            var removed = _items.Remove(item);
            if (removed)
                _onRemovedEvent.Invoke(item);
            return removed;
        }

        public int Count => _items.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public int IndexOf(R item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, R item)
        {
            if (_items[index] != item)
            {
                if (_items[index] != null)
                    _onRemovedEvent.Invoke(item);
                _onAddedEvent.Invoke(item);
                _items.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            if (_items[index] != null)
                _onRemovedEvent.Invoke(_items[index]);
            _items.RemoveAt(index);
        }

        public R this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }
    }
}