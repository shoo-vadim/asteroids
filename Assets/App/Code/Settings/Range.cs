using System;
using UnityEngine;

namespace App.Code.Settings
{
    [Serializable]
    public class Range<T>
    {
        [field: SerializeField] public T Min { get; private set; }
        [field: SerializeField] public T Max { get; private set; }

        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
}