using Inventory_System;
using UnityEngine;

namespace Interactable_object
{
    public class ItemNeeded : MonoBehaviour
    {
        [SerializeField] private InventoryItem _key;

        [Header("Position and Rotation for Item")] 
        [SerializeField] private Transform _keyTransform;
        

        public InventoryItem NeededItem => _key;

        public void Enable() => gameObject.tag = Utilities.Constants.NeededItemTag;

        public void SetupItem()
        {
            GameObject keyGO = Instantiate(_key.gameObject, _keyTransform.position, 
                _keyTransform.rotation, transform);
            keyGO.transform.localScale = _keyTransform.localScale;
        }
    }
}
