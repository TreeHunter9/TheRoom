using UnityEngine;
using Zenject;

namespace TheRoom.Intro
{
    public class TutorialTip : MonoBehaviour
    {
        [SerializeField] private int _invokeCount;
        
        [Inject] private TutorialController _tutorialController;

        public void ShowTip()
        {
            if (_invokeCount <= 0)
                return;
            _tutorialController.ShowNextTip();
            _invokeCount--;
        }
    }
}
