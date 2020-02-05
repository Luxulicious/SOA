using System;
using SOA.Base;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public interface IDecorator<T>
    {
        void OnChange(T currentValue);
        void OnChangeWithHistory(T currentValue, T prevValue);
    }
    
    public abstract class Decorator<T> : ScriptableObject, IDecorator<T>
    {
        public abstract void OnChange(T currentValue);
        public abstract void OnChangeWithHistory(T currentValue, T prevValue);
    }

    [Serializable]
    public abstract class AdditionalOnChangeDecorator<T, E, EE> : Decorator<T>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        [SerializeField] protected E OnChangeEvent = new E();
        [SerializeField] protected EE OnChangeWithHistoryEvent = new EE();
    }

    [Serializable]
    public abstract class ConditionalAdditionalOnChangeDecorator<T, E, EE> : AdditionalOnChangeDecorator<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        public override void OnChange(T currentValue)
        {
            if (Condition(currentValue))
                OnChangeEvent.Invoke(currentValue);
        }

        public override void OnChangeWithHistory(T currentValue, T prevValue)
        {
            if (Condition(currentValue, prevValue))
                OnChangeWithHistoryEvent.Invoke(currentValue, prevValue);
        }

        protected abstract bool Condition(T currentValue, T prevValue);

        protected abstract bool Condition(T value);
    }
}