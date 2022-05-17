using UnityEngine;
using TheRoom.Utilities;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects
{
    [RequireComponent(typeof(RotatableObject))]
    public class KeyController : MonoBehaviour
    {
        [Range(0, 3600)]
        [SerializeField] private float _rotateOnAngle;
        [SerializeField] private bool _destroyAfterUnlock;
        [SerializeField] private bool _rotateWithKeyhole;
        [SerializeField] private bool _destroyKeyholeAfterUnlook;
        [SerializeField] private UnityEvent _actionsAfterUnlock;

        private float _currentAngle = 0f;

        private Quaternion _lastFrameRotation;
        private Quaternion _currentFrameRotation;
        
        private Transform _keyhole;

        private RotatableObject _rotatableObjectScript;

        private void Awake()
        {
            _keyhole = transform.parent;
            if (_rotateWithKeyhole == true)
            {
                transform.SetParent(_keyhole.parent);
                _keyhole.SetParent(transform);
            }
            _rotatableObjectScript = GetComponent<RotatableObject>();
            _rotatableObjectScript.onChangeActive += ChangeActivity;

            this.enabled = false;
        }

        private void OnDestroy()
        {
            _rotatableObjectScript.onChangeActive -= ChangeActivity;
        }

        private void Update()
        {
            _currentFrameRotation = transform.parent.localRotation;
            _currentAngle += CalculateRotationAngle();
            if (Mathf.Abs(_currentAngle) >= _rotateOnAngle)
                Done();
            _lastFrameRotation = transform.parent.localRotation;
        }

        private float CalculateRotationAngle()
        {
            float angle = Quaternion.Angle(_currentFrameRotation, _lastFrameRotation);
            float kindaDirection = (_currentFrameRotation.eulerAngles.x - _lastFrameRotation.eulerAngles.x) +
                                   (_currentFrameRotation.eulerAngles.y - _lastFrameRotation.eulerAngles.y) +
                                   (_currentFrameRotation.eulerAngles.z - _lastFrameRotation.eulerAngles.z);
            
            return kindaDirection > 0 ? angle : angle * -1;
        }

        private void Done()
        {
            _rotatableObjectScript.StopInteraction();
            _actionsAfterUnlock?.Invoke();
            if (_rotateWithKeyhole == true && _destroyKeyholeAfterUnlook == false)
            {
                _keyhole.parent = transform.parent;
            }

            if (_destroyAfterUnlock == false)
            {
                Destroy(this);
                Destroy(_rotatableObjectScript);
            }
            else
                Destroy(gameObject);
        }

        private void ChangeActivity(bool active)
        {
            _currentFrameRotation = transform.parent.localRotation;
            _lastFrameRotation = transform.parent.localRotation;
            this.enabled = true;
        }
    }
}
