using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TheRoom.Animation
{
    public class ChangeImageAlpha : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _animationCurve;
        private Image _image;
        

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            StartCoroutine(FadeAnimation());
        }

        private IEnumerator FadeAnimation()
        {
            float t = 1f;
            while (t >= 0f)
            {
                t -= Time.deltaTime / 2f;
                _image.color = new Color(0, 0, 0, _animationCurve.Evaluate(t));
                yield return null;
            }
        }
    }
}
