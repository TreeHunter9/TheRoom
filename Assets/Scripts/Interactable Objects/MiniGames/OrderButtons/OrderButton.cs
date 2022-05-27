using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TheRoom.InteractableObjects.MiniGames.OrderButtons
{
    public enum StateOfActivity
    {
        Active,
        Passive
    }
    
    public class OrderButton : InteractableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Vector3 _offset;
        [SerializeField, ColorUsage(true, true)] private Color _emissionColor;
        private Vector3 _endPosition;
        private Vector3 _startPosition;
        private bool _isAtStartPosition = true;
        private bool _isAnimated = false;
        private StateOfActivity _stateOfActivity = StateOfActivity.Passive;

        private Material _material;

        public event Action<int, StateOfActivity> onButtonPress;
        
        private void Awake()
        {
            _startPosition = transform.localPosition;
            _endPosition = _startPosition - _offset;
            _material = GetComponent<MeshRenderer>().materials[1];
        }
        
        public override void StartInteraction(Vector3 startPos = default)
        {
            isActive = true;
            ChangeState();
            if (_isAnimated == false)
                MoveAnimation();
            onButtonPress?.Invoke(_id, _stateOfActivity);
        }

        public override void StopInteraction() { }

        private async UniTask MoveAnimation()
        {
            _isAnimated = true;
            float t = _isAtStartPosition == true ? 1 : 0;
            while (true)
            {
                t += _isAtStartPosition == false ? Time.deltaTime * 2 : -1 * Time.deltaTime * 2;
                transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, t);
                _material.SetColor("_Emission", Color.Lerp(Color.black, _emissionColor, t));
                if (t <= 0f || t >= 1f)
                    break;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }

            _isAnimated = false;
        }

        private void ChangeState()
        {
            _stateOfActivity = _stateOfActivity == StateOfActivity.Passive ? StateOfActivity.Active : StateOfActivity.Passive;

            _isAtStartPosition = !_isAtStartPosition;
        }

        public void Refresh()
        {
            if (_isAtStartPosition == true) 
                return;
            ChangeState();
            MoveAnimation();
        }

        public async UniTask DestroyAsync()
        {
            if (_isAnimated == true)
                await UniTask.WaitUntil(() => _isAnimated == false);
            Destroy(this);
        }
    }
}
