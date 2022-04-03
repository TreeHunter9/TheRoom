using UnityEngine;

namespace TheRoom.InventorySystem.Interaction
{
    public class FollowMouse : MonoBehaviour
    {
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}
