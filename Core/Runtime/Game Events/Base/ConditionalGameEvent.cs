using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable]
    public enum ComparisonOperator
    {
        Equals,
        LessThan,
        GreaterThan,
        Unequal
    }

    [Serializable]
    public class Condition<T>
    {
        [SerializeField] protected ComparisonOperator _operator;

        [SerializeField] protected T _valueToCompare;

        public virtual bool Compare(T value)
        {
            var comparison = Comparer<T>.Default.Compare(value, _valueToCompare);
            switch (_operator)
            {
                case ComparisonOperator.Equals:
                    return comparison == 0;
                case ComparisonOperator.LessThan:
                    return comparison < 0;
                case ComparisonOperator.GreaterThan:
                    return comparison > 0;
                case ComparisonOperator.Unequal:
                    return comparison != 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    //TODO This class can't currently be implemented properly since <T>GameEventListeners won't pick this as a valid ScriptableObject
    [Obsolete(
        "TODO This class can't currently be implemented properly since <T>GameEventListeners won't pick this as a valid ScriptableObject")]
    public abstract class ConditionalGameEvent<C, E, T> : GameEvent<E, T>
        where C : Condition<T> where E : UnityEvent<T>, new()
    {
        [SerializeField] protected C[] _conditions;

        /// <summary>
        /// Iterates over conditions and applies AND between them
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool Compare(T value)
        {
            foreach (var c in _conditions)
            {
                if (!c.Compare(value))
                    return false;
            }

            return true;
        }

        public override void Invoke(T value)
        {
            if (Compare(value))
                base.Invoke(value);
        }
    }
}