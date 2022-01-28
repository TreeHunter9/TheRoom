using UnityEngine;

namespace Utilities
{
    public static class ExtensionMethods
    {
        public static Vector3Int Invert(this Vector3Int _vector3Int)
        {
            int x = (int)Mathf.Clamp01(_vector3Int.x);
            int y = (int)Mathf.Clamp01(_vector3Int.y);
            int z = (int)Mathf.Clamp01(_vector3Int.z);
        
            x = x == 0 ? 1 : 0;
            y = y == 0 ? 1 : 0;
            z = z == 0 ? 1 : 0;
        
            return new Vector3Int(x, y, z);
        }
    }
}
