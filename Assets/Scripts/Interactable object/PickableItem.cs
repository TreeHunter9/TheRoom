using Inventory_System;
using UnityEngine;
using UnityEngine.Events;

namespace Interactable_object
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private UnityEvent _actionsAfterPickUp;

        public void TakeItem()
        {
            _inventoryChannel.RaiseLootItemEvent(_inventoryItem);
            _actionsAfterPickUp?.Invoke();
            Destroy(gameObject);
        }

        public void EnableItem() => gameObject.tag = Utilities.Constants.InteractableTag;
    }
}
