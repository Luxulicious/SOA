using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Conditional Int Event",
        menuName = "SOA/Primitives/Int/Conditional Event (Experimental)", order = 1)]
    public class IntConditionalGameEvent : IntGameEvent
    {
        [SerializeField] protected IntCondition[] _conditions;

        //TODO Make compare- and invoke methods generic
        /// <summary>
        /// Iterates over conditions and applies AND between them
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool Compare(int value)
        {
            foreach (var c in _conditions)
            {
                if (!c.Compare(value))
                    return false;
            }

            return true;
        }

        public override void Invoke(int value)
        {
            if (Compare(value))
                base.Invoke(value);
        }
    }

    [Serializable]
    public class IntCondition : Condition<int>
    {
    }
}