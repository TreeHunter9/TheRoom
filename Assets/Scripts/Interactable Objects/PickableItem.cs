using TheRoom.InventorySystem.Core;
using TheRoom.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItemType inventoryItemType;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private UnityEvent _actionsAfterPickUp;

        private static float yAxisCoordinate = -1000f;

        private GameObject CreateInstanceOfItem()
        {
            yAxisCoordinate += 30f;
            Vector3 pos = new Vector3(1000, yAxisCoordinate, 1000);
            return Instantiate(inventoryItemType.gameObjectForSpawn, pos, inventoryItemType.gameObjectForSpawn.transform.rotation);
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
