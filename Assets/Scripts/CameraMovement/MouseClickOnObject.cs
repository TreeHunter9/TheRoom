using System;
using Cinemachine;
using TheRoom.InteractableObjects;
using TheRoom.Utilities;
using UnityEngine;

namespace TheRoom.CameraMovement
{
    public class MouseClickOnObject : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _defaultCM;
        
        private Camera _mainCamera;

        private const float TimeClickTreshold = 0.3f;
        private float _clickTime = 0f;

        private bool _isInteracts = false;
        private void StartInteract() => _isInteracts = true;
        private void StopInteract() => _isInteracts = false;

        private InteractableObject _interactableObject;

        public static event Action onStartInteractionWithObject;
        public static event Action onStopInteractionWithObject;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            onStartInteractionWithObject += StartInteract;
            onStopInteractionWithObject += StopInteract;
        }

        private void OnDisable()
        {
            onStartInteractionWithObject -= StartInteract;
            onStopInteractionWithObject -= StopInteract;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.realtimeSinceStartup - _clickTime <= TimeClickTreshold
                                            && _clickTime != 0f)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    if (hit.collider.TryGetComponent(out HasCamera hasCamera))
                    {
                        hasCamera.ChangeCameraView();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    if (hit.collider.CompareTag(Constants.InteractableTag))
                    {
                        if (hit.collider.TryGetComponent(out _interactableObject))
                        {
                            onStartInteractionWithObject?.Invoke();

                            _interactableObject.StartInteraction(hit.point);
                        }
                        else if (hit.collider.TryGetComponent(out PickableItem pickableItem))
                        {
                            pickableItem.TakeItem();
                        }
                    }
                }

                _clickTime = Time.realtimeSinceStartup;
            }
            else if (Input.GetMouseButtonUp(0) && _isInteracts == true)
            {
                onStopInteractionWithObject?.Invoke();
                if (_interactableObject != null)
                    _interactableObject.StopInteraction();
            }
        }
    }
}
