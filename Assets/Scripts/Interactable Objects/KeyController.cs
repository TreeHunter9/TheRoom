using UnityEngine;
using TheRoom.Utilities;

namespace TheRoom.InteractableObjects
{
    [RequireComponent(typeof(RotatableObject))]
    public class KeyController : MonoBehaviour
    {
        [Range(0, 3600)]
        [SerializeField] private float _rotateOnAngle;
        [SerializeField] private bool _rotateWithKeyhole;
        [SerializeField] private bool _destroyKeyholeAfterUnlook;

        private float _currentAngle = 0f;

        private Quaternion _lastFrameRotation;
        private Quaternion _currentFrameRotation;
        
        private Transform _keyhole;

        private RotatableObject _rotatableObjectScript;

        private void Awake()
        {
            _keyhole = transform.parent.parent;
            if (_rotateWithKeyhole == true)
            {
                transform.SetParent(transform.parent.parent.parent);
                _keyhole.SetParent(transform);
            }
            _rotatableObjectScript = GetComponent<RotatableObject>();
            _rotatableObjectScript.onChangeActive += active => this.enabled = active;

            _lastFrameRotation = transform.rotation;
            _currentFrameRotation = transform.rotation;
            
            this.enabled = false;
        }

        private void OnDestroy()
        {
            _rotatableObjectScript.onChangeActive -= active => this.enabled = active;
        }

        private void Update()
        {
            _currentFrameRotation = transform.rotation;
            _currentAngle += CalculateRotationAngle();
            if (Mathf.Abs(_currentAngle) >= _rotateOnAngle)
                Done();
            _lastFrameRotation = transform.rotation;
        }

        private float CalculateRotationAngle()
        {
            float angle = Quaternion.Angle(_currentFrameRotation, _lastFrameRotation);
            return _currentFrameRotation.eulerAngles.z - _lastFrameRotation.eulerAngles.z > 0
                ? angle
                : angle * -1;
        }

        private void Done()
        {
            _rotatableObjectScript.StopInteraction();
            _keyhole.GetComponent<KeyholeAction>().RaiseAction();
            if (_rotateWithKeyhole == true && _destroyKeyholeAfterUnlook == false)
            {
                _keyhole.parent = transform.parent;
            }
            Destroy(gameObject);
        }
    }
}
