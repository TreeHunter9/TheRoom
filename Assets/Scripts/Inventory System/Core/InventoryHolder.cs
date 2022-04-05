using Cinemachine;
using UnityEngine;
using Zenject;

namespace TheRoom.InventorySystem.Core
{
    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private int _slotsCount;
        
        [Inject] private InventoryChannel _inventoryChannel;

        private Inventory _inventory = new Inventory();
        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventoryChannel.onInventoryItemLoot += InventoryItemLoot;
            for (int i = 0; i < _slotsCount; i++)
            {
                _inventory.CreateSlot();
            }
        }

        private void OnDisable()
        {
            _inventoryChannel.onInventoryItemLoot -= InventoryItemLoot;
        }

        private void InventoryItemLoot(InventoryItemType itemType, GameObject itemGO)
        {
            InventorySlot slot = _inventory.FindEmptySlot();
            slot.Item = new InventoryItem
            (
                itemType,
                itemGO,
                itemGO.GetComponentInChildren<CinemachineFreeLook>()
            );
        }
    }
}
