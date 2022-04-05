using UnityEngine;

namespace TheRoom.Utilities
{
    public static class UtilitiesMethods
    {
        public static Vector3 Multiplication(params Vector3[] vectors)
        {
            Vector3 result = Vector3.one;
            for (int i = 0; i < vectors.Length; i++)
            {
                result.x *= vectors[i].x;
                result.y *= vectors[i].y;
                result.z *= vectors[i].z;
            }

            return result;
        }
    }
}