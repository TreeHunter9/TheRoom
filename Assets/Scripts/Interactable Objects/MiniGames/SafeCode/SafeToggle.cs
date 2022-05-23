using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TheRoom.InteractableObjects.MiniGames.SafeCode
{
    public class SafeToggle : MonoBehaviour
    {
        [SerializeField] private int _id;

        private SafeDataHolder _safeDataHolder;
        private Transform _arrowTransform;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Vector3 _offset;
        
        private bool _isAtStartPosition = true;
        private bool _isAnimated = false;

        [Inject]
        private void Constructor(SafeDataHolder safeDataHolder)
        {
            _safeDataHolder = safeDataHolder;
            _safeDataHolder.onComplete += CompleteAsync;
            _safeDataHolder.onFailed += Refresh;
            _safeDataHolder.onActiveChange += ChangeActivityState;
        }

        private void Awake()
        {
            _startPosition = transform.localPosition;
            _offset = transform.parent.InverseTransformDirection(transform.forward) * 0.00009f;
            _endPosition = _startPosition + _offset;
            _arrowTransform = _safeDataHolder.arrowTransform;
            this.enabled = false;
        }

        private void Update()
        {
            Vector3 rotation1 = transform.rotation * Vector3.forward;
            Vector3 rotation2 = _arrowTransform.rotation * Vector3.forward;
            float angle = Vector3.Angle(rotation1, rotation2);
            if (angle <= 4.5f)
                Activate();
        }

        private void Activate()
        {
            bool isConditionSatisfied = _safeDataHolder.CheckCondition(_id);
            if (isConditionSatisfied == false)
                return;
            _isAtStartPosition = false;
            if (_isAnimated == false)
                PlayAnimationAsync();
            this.enabled = false;
        }

        private async void CompleteAsync()
        {
            _safeDataHolder.onComplete -= CompleteAsync;
            _safeDataHolder.onFailed -= Refresh;
            if (_isAnimated == true || _isAtStartPosition == true)
                await UniTask.WaitUntil(() => _isAnimated == false);
            Destroy(this);
        }

        private void Refresh()
        {
            if (_isAtStartPosition == true)
                return;
            _isAtStartPosition = true;
            if (_isAnimated == false)
                PlayAnimationAsync();
            this.enabled = true;
        }

        private async UniTask PlayAnimationAsync()
        {
            _isAnimated = true;
            float t = _isAtStartPosition == true ? 1 : 0;
            while (true)
            {
                t += _isAtStartPosition == false ? Time.deltaTime * 2 : -1 * Time.deltaTime * 2;
                transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, t);
                if (t <= 0f || t >= 1f)
                    break;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }

            _isAnimated = false;
        }

        private void ChangeActivityState(bool active)
        {
            if (_isAtStartPosition == true)
                this.enabled = active;
        }
    }
}
