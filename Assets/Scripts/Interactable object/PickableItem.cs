using Inventory_System;
using UnityEngine;

namespace Interactable_object
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private InventoryChannel _inventoryChannel;

        public void TakeItem()
        {
            _inventoryChannel.RaiseLootItem(_inventoryItem);
            Destroy(gameObject);
        }

        public void EnableItem() => gameObject.tag = Utilities.Constants.InteractableTag;
    }
}
