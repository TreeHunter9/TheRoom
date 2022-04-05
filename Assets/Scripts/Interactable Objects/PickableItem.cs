using TheRoom.InventorySystem.Core;
using TheRoom.Utilities;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace TheRoom.InteractableObjects
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItemType inventoryItemType;
        [SerializeField] private UnityEvent _actionsAfterPickUp;

        [Inject] private InventoryChannel _inventoryChannel;
        
        private static float yAxisCoordinate = -1000f;

        private GameObject CreateInstanceOfItem()
        {
            yAxisCoordinate += 30f;
            Vector3 pos = new Vector3(1000, yAxisCoordinate, 1000);
            return Instantiate(inventoryItemType.prefabForLook, pos, inventoryItemType.prefabForLook.transform.rotation);
        }

        public void TakeItem()
        {
            GameObject itemGO = CreateInstanceOfItem();
            _inventoryChannel.RaiseLootItemEvent(inventoryItemType, itemGO);
            _actionsAfterPickUp?.Invoke();
            Destroy(gameObject);
        }

        public void EnableItem() => gameObject.tag = Constants.InteractableTag;
    }
}
