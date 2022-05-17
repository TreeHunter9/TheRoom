using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace TheRoom.Animation.Editor
{
    [CustomEditor(typeof(SimpleAnimation))]
    public class SimpleAnimationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Animator animator = (Animator) serializedObject.FindProperty("_animator").objectReferenceValue;
            int listCount = serializedObject.FindProperty("_animationCount").intValue;
            if (animator == null)
                return;
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
            SimpleAnimation simpleAnimationScript = (SimpleAnimation) target;
            List<string> animationList = new List<string>();
            for (int i = 0; i < listCount; i++)
            {
                int index = 0;
                int currentIndex = 0;
                List<string> stateNames = new List<string>();
                foreach (AnimatorControllerLayer animatorControllerLayer in animatorController.layers)
                {
                    string layerName = animatorControllerLayer.name;
                    foreach (ChildAnimatorState childAnimatorState in animatorControllerLayer.stateMachine.states)
                    {
                        if (childAnimatorState.state.name == "Empty State")
                            continue;
                        string stateName = layerName + "." + childAnimatorState.state.name;
                        if (simpleAnimationScript.animationList.Count > i
                            && simpleAnimationScript.animationList[i] == stateName)
                            currentIndex = index;
                        stateNames.Add(stateName);
                        index++;
                    }
                }
                currentIndex = EditorGUILayout.Popup($"Animation name {i + 1}", currentIndex, stateNames.ToArray());
                animationList.Add(stateNames[currentIndex]);
            }

            simpleAnimationScript.animationList = animationList;
        }
    }
}
