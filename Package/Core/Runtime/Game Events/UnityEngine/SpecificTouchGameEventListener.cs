using System;
using System.Linq;
using SOA.Common.UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    //TODO Move this to SOA (this could also be a decorator)
    [Serializable]
    public class SpecificTouchGameEventListener : TouchGameEventListener
    {
        //TODO WIP
        [Tooltip("If true filters cannot be edited within the inspector")]
        protected bool _lockFiltersFromInspector = false;

        [Tooltip("Empty means no filter is applied")]
        [SerializeField]
        protected int[] fingerIdsFilter = new[] { 0 };

        [Tooltip("Empty means no filter is applied")]
        [SerializeField]
        protected TouchPhase[] touchPhasesFilter = new TouchPhase[]
            {TouchPhase.Began, TouchPhase.Canceled, TouchPhase.Ended, TouchPhase.Moved, TouchPhase.Stationary};


        public SpecificTouchGameEventListener(bool lockFiltersFromInspector)
        {
            throw new NotImplementedException();
            _lockFiltersFromInspector = lockFiltersFromInspector;
        }

        public SpecificTouchGameEventListener()
        {
        }

        public SpecificTouchGameEventListener FilterOnlyFirstTouch()
        {
            fingerIdsFilter = new[] { 0 };
            return this;
        }

        public SpecificTouchGameEventListener FilterOnlyTouchPhase(TouchPhase touchPhase)
        {
            touchPhasesFilter = new TouchPhase[] { touchPhase };
            return this;
        }

        public SpecificTouchGameEventListener FilterOnlyActiveTouches()
        {
            touchPhasesFilter = new TouchPhase[] { TouchPhase.Began, TouchPhase.Moved, TouchPhase.Stationary };
            return this;
        }

        public SpecificTouchGameEventListener FilterOnlyInactiveTouches()
        {
            touchPhasesFilter = new TouchPhase[] { TouchPhase.Ended, TouchPhase.Canceled };
            return this;
        }

        protected override void InvokeResponses(Touch value)
        {
            if ((fingerIdsFilter.Contains(value.fingerId) || fingerIdsFilter.Length < 1) &&
                touchPhasesFilter.Contains(value.phase) || touchPhasesFilter.Length < 1)
                base.InvokeResponses(value);
        }
    }
}