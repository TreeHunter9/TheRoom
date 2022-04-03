using UnityEngine;

namespace TheRoom.Animation
{
    public class SimpleAnimation : MonoBehaviour
    {
        private UnityEngine.Animation _animation;

        private void Awake()
        {
            _animation = GetComponent<UnityEngine.Animation>();
        }

        public void StartAnimation() => _animation.Play();
    }
}
