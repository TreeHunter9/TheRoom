using Cinemachine;
using UnityEngine;

namespace TheRoom.InventorySystem.Core
{
    public class InventoryItem
    {
        private InventoryItemType _itemType;
        private GameObject _itemGO;
        private CinemachineFreeLook _cinemachineFreeLook;

        public InventoryItem(InventoryItemType itemType, GameObject itemGO, CinemachineFreeLook cinemachineFreeLook)
        {
            _itemType = itemType;
            _itemGO = itemGO;
            _cinemachineFreeLook = cinemachineFreeLook;
        }

        public InventoryItemType ItemType => _itemType;

        public GameObject ItemGO => _itemGO;
        
        public CinemachineFreeLook CinemachineFreeLook => _cinemachineFreeLook;

        public Sprite Sprite => _itemType.sprite;
    }
}