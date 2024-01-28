using System.Collections.Generic;
using App.Code.Model.Proto;

namespace App.Code.Tools
{
    public static class Extensions
    {
        public static IEnumerable<int> GetNotEmptyIndexes(this Entity[] entities)
        {
            for (var i = 0; i < entities.Length; i++)
            {
                if (entities[i] != null)
                {
                    yield return i;
                }
            }
        }
    }
}