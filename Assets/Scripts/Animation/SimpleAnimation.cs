using System.Collections.Generic;
using UnityEngine;

namespace TheRoom.Animation
{
    [RequireComponent(typeof(UnityEngine.Animation))]
    public class SimpleAnimation : MonoBehaviour
    {
        [SerializeField] private List<AnimationClip> _animationClips;
        
        private UnityEngine.Animation _animation;

        private void Awake()
        {
            _animation = GetComponent<UnityEngine.Animation>();
        }

        public void StartAnimation()
        {
            _animation.clip = _animationClips[0];
            _animationClips.RemoveAt(0);
            _animation.Play();
        }
    }
}
