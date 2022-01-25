using UnityEngine;

namespace Inventory_System
{
    public class FollowMouse : MonoBehaviour
    {
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
