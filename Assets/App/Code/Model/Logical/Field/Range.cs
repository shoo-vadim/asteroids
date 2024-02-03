namespace App.Code.Model.Logical.Field
{
    public struct Range<T>
    {
        public readonly T Min;
        public readonly T Max;

        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }
}