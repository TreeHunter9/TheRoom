using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TheRoom.InteractableObjects.MiniGames.TimeButtons
{
    public class TimePanel : MonoBehaviour
    {
        [SerializeField] private ButtonObject _button;
        [SerializeField] private Transform _woodBlock;
        [SerializeField] private Transform _nextWoodBlock;
        [SerializeField] private TimePanel _nextPanel;
        [SerializeField] private Transform _arrow;

        private Quaternion _woodStartRotation;
        private Quaternion _woodEndRotation;
        
        private Quaternion _nextWoodEndRotation;
        
        private const float WoodRotateAngle = 151f;

        public event Action onOpenNextWoodBlock;

        private void Awake()
        {
            if (_woodBlock != null)
            {
                _woodStartRotation = _woodBlock.localRotation;
                _woodEndRotation = _woodStartRotation * Quaternion.Euler(Vector3.right * WoodRotateAngle);
            }

            if (_nextWoodBlock != null)
                _nextWoodEndRotation = _nextWoodBlock.localRotation * Quaternion.Euler(Vector3.right * WoodRotateAngle);
            

            if (_nextPanel != null)
                _nextPanel.onOpenNextWoodBlock += ClosePanel;
            _button.onButtonRelease += ButtonPress;
        }

        private void OnDisable()
        {
            if (_nextPanel != null)
                _nextPanel.onOpenNextWoodBlock -= ClosePanel;
            _button.onButtonRelease -= ButtonPress;
        }

        private void ButtonPress()
        {
            RotateWoodBlockAsync(_nextWoodBlock, _nextWoodEndRotation);
            onOpenNextWoodBlock?.Invoke();
        }

        public async UniTask RotateWoodBlockAsync(Transform woodBlock, Quaternion endRotation)
        {
            while (true)
            {
                woodBlock.localRotation = Quaternion.RotateTowards(woodBlock.localRotation, endRotation, 
                    Time.deltaTime * 500);
                if (woodBlock.localRotation == endRotation)
                    break;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
        }

        public void ClosePanel()
        {
            if (_woodBlock == null)
                return;
            RotateWoodBlockAsync(_woodBlock, _woodStartRotation);
            _button.SetActive(true);
        }
        
        public Transform GetArrow() => _arrow;
    }
}
