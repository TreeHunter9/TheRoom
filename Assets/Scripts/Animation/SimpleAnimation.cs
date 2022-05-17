using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheRoom.Animation
{
    public class SimpleAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _animationCount = 1;
        [HideInInspector] public List<string> animationList;

        public void StartAnimation()
        {
            _animator.Play(animationList[0]);
            animationList.RemoveAt(0);
        }
    }
}
