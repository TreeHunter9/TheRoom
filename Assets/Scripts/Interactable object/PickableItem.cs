using Inventory_System;
using UnityEngine;
using UnityEngine.Events;

namespace Interactable_object
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private GameObject _prefabForLook;
        [SerializeField] private UnityEvent _actionsAfterPickUp;

        private static float yAxisCoordinate = -1000f;

        private GameObject CreateInstanceOfItem()
        {
            yAxisCoordinate += 30f;
            Vector3 pos = new Vector3(1000, yAxisCoordinate, 1000);
            return Instantiate(_prefabForLook, pos, _prefabForLook.transform.rotation);
        }

        public void TakeItem()
        {
            GameObject itemGO = CreateInstanceOfItem();
            _inventoryChannel.RaiseLootItemEvent(_inventoryItem, itemGO);
            _actionsAfterPickUp?.Invoke();
            Destroy(gameObject);
        }

        public void EnableItem() => gameObject.tag = Utilities.Constants.InteractableTag;
    }
}
