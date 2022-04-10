using System.Collections.Generic;
using TheRoom.Utilities;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class InteractList : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _interactableObjectList;
        [SerializeField] private List<GameObject> _itemNeededObjects;

        private void RemoveNullableObjects()
        {
            _interactableObjectList.RemoveAll(item => item == null);
            _itemNeededObjects.RemoveAll(item => item == null);
        }

        public void EnableObjects()
        {
            RemoveNullableObjects();
            foreach (var interactableObject in _interactableObjectList)
            {
                interactableObject.tag = Constants.InteractableTag;
            }
            foreach (var itemNeededObject in _itemNeededObjects)
            {
                itemNeededObject.tag = Constants.NeededItemTag;
            }
        }
        
        public void DisableObjects()
        {
            RemoveNullableObjects();
            foreach (var interactableObject in _interactableObjectList)
            {
                interactableObject.tag = Constants.NonTag;
            }
            foreach (var itemNeededObject in _itemNeededObjects)
            {
                itemNeededObject.tag = Constants.NonTag;
            }
        }
    }
}
