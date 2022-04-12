using Cinemachine;
using TheRoom.InventorySystem.Core;
using UnityEngine;
using Zenject;

namespace TheRoom.CameraMovement
{
    [RequireComponent(typeof(CinemachineBrain))]
    public class CinemachineCameraHelper : MonoBehaviour
    {
        [Inject] private InventoryCursorChannel _inventoryCursorChannel;
        
        private static CinemachineBrain _cinemachineBrain;
        private static CinemachineFreeLook _cinemachineSave;

        private void Awake()
        {
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            _inventoryCursorChannel.onItemSlotClick += item => ChangeAndSaveCamera(item.CinemachineFreeLook);
        }

        private static void ChangeAndSaveCamera(CinemachineFreeLook toCM)
        {
            if (_cinemachineSave == null)
                _cinemachineSave = (CinemachineFreeLook)_cinemachineBrain.ActiveVirtualCamera;
            else if (_cinemachineBrain.ActiveVirtualCamera.Name != toCM.Name)
                _cinemachineSave = (CinemachineFreeLook)_cinemachineBrain.ActiveVirtualCamera;
            
            ChangeCamera(toCM);
        }

        public static void ChangeCamera(CinemachineFreeLook toCM)
        {
            _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            toCM.Priority = 1;
        }

        public static void RefreshCamera(CinemachineFreeLook toCM)
        {
            if (_cinemachineSave == null)
            {
                ChangeCamera(toCM);
                return;
            }
            
            _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            _cinemachineSave.Priority = 1;
            _cinemachineSave.m_Transitions.m_InheritPosition = false;
            _cinemachineSave = null;
        }
    }
}
