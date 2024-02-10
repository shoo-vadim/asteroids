using System.Collections.Generic;
using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Logical.Extensions
{
    public static class Extensions
    {
        public static float GetRandom(this Range<float> range) => Random.Range(range.Min, range.Max);

        public static TValue GetAndRemove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            var value = dictionary[key];
            dictionary.Remove(key);
            return value;
        }
    }
}