using System;
using UnityEngine;

namespace TheRoom.InteractableObjects.MiniGames.Lasers
{
    [Serializable]
    public class ReflectionData
    {
        public string reflectionHistory;
        public Vector3 reflectPosition;

        public ReflectionData(string reflectionHistory, Vector3 reflectPosition)
        {
            this.reflectionHistory = reflectionHistory;
            this.reflectPosition = reflectPosition;
        }
    }
}