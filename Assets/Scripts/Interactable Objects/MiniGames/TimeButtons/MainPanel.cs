using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TheRoom.InteractableObjects;
using TheRoom.InteractableObjects.MiniGames.TimeButtons;
using UnityEngine;

namespace TheRoom
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private List<TimePanel> _timePanels;
        [SerializeField] private ButtonObject _button1;
        [SerializeField] private ButtonObject _button2;
        [SerializeField] private List<Transform> _woodBlocks;

        private List<Transform> _arrows;
        
        private Quaternion _arrowStartRotation;
        private Quaternion _arrowEndRotation;
        private Quaternion _arrowCurrentRotation;
        private const float ArrowRotateAngle = 107f;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _arrows = new List<Transform>(_timePanels.Count);
            foreach (TimePanel timePanel in _timePanels)
            {
                timePanel.onOpenNextWoodBlock += RefreshTimer;
                _arrows.Add(timePanel.GetArrow());
            }

            _arrowStartRotation = _arrows[0].localRotation;
            _arrowEndRotation = _arrowStartRotation * Quaternion.Euler(Vector3.forward * ArrowRotateAngle);
            _arrowCurrentRotation = _arrowStartRotation;

            _button1.onButtonRelease += Button1WasPressed;
            _button2.onButtonRelease += Button2WasPressed;
        }

        private void OnDisable()
        {
            foreach (TimePanel timePanel in _timePanels)
            {
                timePanel.onOpenNextWoodBlock -= RefreshTimer;
            }

            _button1.onButtonRelease -= Button1WasPressed;
            _button2.onButtonRelease -= Button2WasPressed;
        }

        private async UniTask MoveArrowAsync(CancellationToken token)
        {
            while (true)
            {
                _arrowCurrentRotation = Quaternion.RotateTowards(_arrowCurrentRotation, _arrowEndRotation,
                    Time.deltaTime * 7);
                foreach (Transform arrow in _arrows)
                {
                    arrow.localRotation = _arrowCurrentRotation;
                }
                if (_arrowCurrentRotation == _arrowEndRotation)
                {
                    TimeIsOver();
                    break;
                }
                if (token.IsCancellationRequested)
                    break;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            foreach (Transform arrow in _arrows)
            {
                arrow.localRotation = _arrowStartRotation;
            }
        }

        private void TimeIsOver()
        {
            CloseAllPanels();
        }

        private void RefreshTimer()
        {
            _arrowCurrentRotation = _arrowStartRotation;
        }

        private void Button1WasPressed()
        {
            _button2.isEnabled = false;
            StartTimer();
        }

        private void Button2WasPressed()
        { 
            _button1.isEnabled = false;
            StartTimer();
        }

        private void StartTimer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            MoveArrowAsync(_cancellationTokenSource.Token);
        }

        private void StopTimer()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void CloseAllPanels()
        {
            foreach (TimePanel timePanel in _timePanels)
            {
                timePanel.ClosePanel();
            }

            foreach (Transform woodBlock in _woodBlocks)
            {
                if (180f - woodBlock.rotation.eulerAngles.x > -80f)
                    _timePanels[0].RotateWoodBlockAsync(woodBlock,
                        woodBlock.rotation * Quaternion.Euler(Vector3.right * -151f));
            }
            _button1.isEnabled = true;
            _button2.isEnabled = true;
        }

        public void ItemPickUp()
        {
            CloseAllPanels();
            StopTimer();
        }
    }
}
