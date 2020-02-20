using System;
using System.Collections.Generic;
using SOA.Base;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public enum ComparisonOperator
{
    Equals,
    LessThan,
    GreaterThan,
    Unequal
}

public abstract class ConditionalGameEvent<E, T> : GameEvent<E, T> where
    E : UnityEvent<T>, new()
{
    public override void Invoke(T t0)
    {
        base.Invoke(t0);
    }
}

public abstract class ComparableConditionalGameEvent<E, T> : GameEvent<E, T> 
    where E : UnityEvent<T>, new()
{
    [Serializable]
    public class Condition<T>
    {
        [SerializeField] protected ComparisonOperator _operator;

        [SerializeField] protected T _valueToCompare;
        public ComparisonOperator Operator
        {
            get => _operator;
            set => _operator = value;
        }

        public bool Compare(T value)
        {
            var comparison = Comparer<T>.Default.Compare(value, _valueToCompare);
            switch (_operator)
            {
                case ComparisonOperator.Equals:
                    return comparison == 0;
                case ComparisonOperator.LessThan:
                    return comparison > 0;
                case ComparisonOperator.GreaterThan:
                    return comparison < 0;
                case ComparisonOperator.Unequal:
                    return comparison != 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [SerializeField] protected Condition<T>[] _conditions;

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