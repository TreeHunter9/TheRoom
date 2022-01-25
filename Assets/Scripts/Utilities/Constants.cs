using UnityEngine;

namespace Utilities
{
    public static class Constants
    {
        public const string InteractableTag = "Interactable";
        public const string NonTag = "Untagged";
        public const string NeededItemTag = "NeedItem";
        public static readonly int PlaneLayer = LayerMask.NameToLayer("PlaneForRaycast");
    }
}
