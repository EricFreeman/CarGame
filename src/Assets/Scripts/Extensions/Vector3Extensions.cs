using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            return vectors.Aggregate(Vector3.zero, (current, vector3) => current + vector3) / vectors.Count();
        }
    }
}
