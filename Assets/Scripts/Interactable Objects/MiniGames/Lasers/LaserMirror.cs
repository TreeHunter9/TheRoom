using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TheRoom.InteractableObjects.MiniGames.Lasers
{
    public class LaserMirror : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private List<ReflectionData> _reflections;

        public Vector3 GetReflectionDirection(Vector3 point, StringBuilder stringBuilder)
        {
            string reflectionHistory = stringBuilder.ToString(); 
            stringBuilder.Append(_id);
            if (_reflections.Count == 1)
                return _reflections[0].reflectPosition - point;

            foreach (ReflectionData reflectionData in _reflections)
            {
                if (reflectionHistory == reflectionData.reflectionHistory)
                    return reflectionData.reflectPosition - point;
            }
            return Vector3.zero;
        }
    }
}
