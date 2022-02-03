using Cinemachine;
using UnityEngine;

namespace Inventory_System
{
    public class InteractableItem
    {
        private GameObject _itemGO;
        public GameObject ItemGO => _itemGO;

        private CinemachineFreeLook _cinemachineFreeLook;
        public CinemachineFreeLook CinemachineFreeLook => _cinemachineFreeLook;
        
        public float state;

        public InteractableItem(GameObject itemGO, CinemachineFreeLook cinemachineFreeLook)
        {
            _itemGO = itemGO;
            _cinemachineFreeLook = cinemachineFreeLook;
        }
    }
}