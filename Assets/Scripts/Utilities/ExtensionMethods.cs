using UnityEngine;

namespace TheRoom.Utilities
{
    public static class ExtensionMethods
    {
        public static Vector3Int Invert(this Vector3Int vector3Int)
        {
            int x = (int)Mathf.Clamp01(vector3Int.x);
            int y = (int)Mathf.Clamp01(vector3Int.y);
            int z = (int)Mathf.Clamp01(vector3Int.z);
        
            x = x == 0 ? 1 : 0;
            y = y == 0 ? 1 : 0;
            z = z == 0 ? 1 : 0;
        
            return new Vector3Int(x, y, z);
        }

        public static Vector3 ToVector3(this Vector3Int vector3Int) => new Vector3(vector3Int.x, vector3Int.y, vector3Int.z);

        public static Quaternion Restrict(this Quaternion rotation, Vector3 minRot, Vector3 maxRot)
        {
            Vector3 eulerAngles = rotation.eulerAngles;

            eulerAngles.x = eulerAngles.x > 180 ? eulerAngles.x - 360 : eulerAngles.x;
            eulerAngles.y = eulerAngles.y > 180 ? eulerAngles.y - 360 : eulerAngles.y;
            eulerAngles.z = eulerAngles.z > 180 ? eulerAngles.z - 360 : eulerAngles.z;

            eulerAngles.x = Mathf.Clamp(eulerAngles.x, minRot.x, maxRot.x);
            eulerAngles.y = Mathf.Clamp(eulerAngles.y, minRot.y, maxRot.y);
            eulerAngles.z = Mathf.Clamp(eulerAngles.z, minRot.z, maxRot.z);

            return Quaternion.Euler(eulerAngles);
        }
    }
}
