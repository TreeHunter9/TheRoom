using System;
using Cinemachine;
using Inventory_System;
using UnityEngine;

namespace CameraMovement
{
    [RequireComponent(typeof(CinemachineBrain))]
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private InventoryCursorChannel _inventoryCursorChannel;
        
        private static CinemachineBrain _cinemachineBrain;

        private void Awake()
        {
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            _inventoryCursorChannel.onItemSlotClick += item => ChangeCamera(item.CinemachineFreeLook);
        }

        public static void ChangeCamera(CinemachineFreeLook toCM)
        {
            _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            toCM.Priority = 1;
        }
    }
}
