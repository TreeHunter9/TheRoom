using System.Collections.Generic;
using TheRoom.Utilities;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class InteractList : MonoBehaviour
    {
        [SerializeField] private List<InteractableObject> _interactableObjectList;

        public void EnableInteractableObjects()
        {
            _interactableObjectList.RemoveAll(item => item == null);
            foreach (var interactableObject in _interactableObjectList)
            {
                interactableObject.tag = Constants.InteractableTag;
            }
        }
        
        public void DisableInteractableObjects()
        {
            _interactableObjectList.RemoveAll(item => item == null);
            foreach (var interactableObject in _interactableObjectList)
            {
                interactableObject.tag = Constants.NonTag;
            }
        }
    }
}
