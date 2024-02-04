using App.Code.Settings;
using UnityEngine;

namespace App.Code.Model.Logical.Extensions
{
    public static class Extensions
    {
        public static float GetRandom(this Range<float> range) => Random.Range(range.Min, range.Max);
    }
}