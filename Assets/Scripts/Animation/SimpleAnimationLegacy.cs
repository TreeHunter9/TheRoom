using System.Collections.Generic;
using UnityEngine;

namespace TheRoom
{
    [RequireComponent(typeof(UnityEngine.Animation))]
    public class SimpleAnimationLegacy : MonoBehaviour
    {
        private UnityEngine.Animation _animationComponent;
        private List<AnimationClip> _animations;

        private void Awake()
        {
            _animationComponent = GetComponent<UnityEngine.Animation>();
            _animations = new List<AnimationClip>(_animationComponent.GetClipCount());
            foreach (AnimationState animationState in _animationComponent)
            {
                _animations.Add(animationState.clip);
            }
        }

        public void StartAnimation()
        {
            _animationComponent.clip = _animations[0];
            _animationComponent.Play();
            _animations.RemoveAt(0);
        }
    }
}
