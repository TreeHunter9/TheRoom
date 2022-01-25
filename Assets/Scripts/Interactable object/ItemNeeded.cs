using Inventory_System;
using UnityEngine;

namespace Interactable_object
{
    public class ItemNeeded : MonoBehaviour
    {
        [SerializeField] private InventoryItem _key;

        public InventoryItem NeededItem => _key;

        public void Enable() => gameObject.tag = Utilities.Constants.NeededItemTag;
    }
}
