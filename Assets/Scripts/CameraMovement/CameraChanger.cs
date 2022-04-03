using Cinemachine;
using TheRoom.InventorySystem.Core;
using UnityEngine;

namespace TheRoom.CameraMovement
{
    [RequireComponent(typeof(CinemachineBrain))]
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private InventoryCursorChannel _inventoryCursorChannel;
        
        private static CinemachineBrain _cinemachineBrain;
        private static CinemachineFreeLook _cinemachineSave;
        private static Vector3 _saveCameraPosition;

        private void Awake()
        {
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            _inventoryCursorChannel.onItemSlotClick += item => ChangeAndSaveCamera(item.CinemachineFreeLook);
        }

        private static void ChangeAndSaveCamera(CinemachineFreeLook toCM)
        {
            ChangeCamera(toCM);
            _cinemachineSave = (CinemachineFreeLook)_cinemachineBrain.ActiveVirtualCamera;
            _saveCameraPosition = _cinemachineSave.State.FinalPosition;
        }

        public static void ChangeCamera(CinemachineFreeLook toCM)
        {
            if (_cinemachineSave == null)
            {
                _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
                toCM.Priority = 1;
                return;
            }
            
            _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            _cinemachineSave.Priority = 1;
            _cinemachineSave.m_Transitions.m_InheritPosition = false;
            _cinemachineSave = null;
        }
    }
}
