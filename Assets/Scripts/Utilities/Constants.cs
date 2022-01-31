using UnityEngine;

namespace Utilities
{
    public static class Constants
    {
        public const string InteractableTag = "Interactable";
        public const string NonTag = "Untagged";
        public const string NeededItemTag = "NeedItem";
        public const string GOForCameraTag = "GOForCamera";
        public static readonly int PlaneLayer = LayerMask.NameToLayer("PlaneForRaycast");
    }
}
