using System;
using UnityEngine.Events;

namespace TheRoom.Intro
{
    [Serializable]
    public class TipSettings
    {
        public string message;
        public float durationTime;
        public float visibleTimeDelay;
        public UnityEvent actionsOnShowTip;
        public UnityEvent actionsAfterShowTip;
    }
}