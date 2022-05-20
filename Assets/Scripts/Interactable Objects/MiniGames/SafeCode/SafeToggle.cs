using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TheRoom.InteractableObjects.MiniGames.SafeCode
{
    public class SafeToggle : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Vector3 _offset;

        private SafeDataHolder _safeDataHolder;
        private Transform _arrowTransform;
        private Vector3 _endPosition;
        private Vector3 _startPosition;
        private bool _isAtStartPosition = true;
        private bool _isAnimated = false;

        [Inject]
        private void Constructor(SafeDataHolder safeDataHolder)
        {
            _safeDataHolder = safeDataHolder;
            _safeDataHolder.onComplete += CompleteAsync;
            _safeDataHolder.onFailed += Refresh;
        }

        private void Awake()
        {
            _startPosition = transform.localPosition;
            _endPosition = _startPosition + _offset;
            _arrowTransform = _safeDataHolder.arrowTransform;
        }

        private void Update()
        {
            // var rotation1 = transform.rotation * Vector3.back;
            // var rotation2 = _arrowTransform.rotation * Vector3.back;
            //
            // var angle1 = Mathf.Atan2(rotation1.x, rotation1.y) * Mathf.Rad2Deg;
            // var angle2 = Mathf.Atan2(rotation2.x, rotation2.y) * Mathf.Rad2Deg;
            //
            // var angleDiff = Mathf.DeltaAngle(angle1, angle2);
            // print(angleDiff);
            
            // if (angleDiff >= 0f && angleDiff < -20f)
            //     Activate();
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
            if (_isAnimated == true)
                await UniTask.WaitUntil(() => _isAnimated == false);
            Destroy(this);
        }

        private void Refresh()
        {
            _isAtStartPosition = true;
            if (_isAnimated == false)
                PlayAnimationAsync();
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
    }
}
