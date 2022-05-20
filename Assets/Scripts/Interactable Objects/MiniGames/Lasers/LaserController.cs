using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects.MiniGames.Lasers
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField] private Vector3 _firstPoint;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _target;
        [SerializeField] private UnityEvent _actionsOnComplete;
        
        private LineRenderer _lineRenderer;

        private List<Vector3> _points;
        private StringBuilder _stringBuilder;
        private Vector3 _firstPointForRaycast;

        private void Awake()
        {
            _firstPointForRaycast = Vector3.Lerp(transform.position, _firstPoint, 0.5f);
            _lineRenderer = GetComponent<LineRenderer>();
            _stringBuilder = new StringBuilder();
        }

        private void Update()
        {
            _stringBuilder.Clear();
            _points = new List<Vector3>(10) {transform.position, _firstPoint};
            Ray ray = new Ray(_firstPointForRaycast, _firstPoint - transform.position);
            Physics.Raycast(ray, out var hitInfo, 10f, _layerMask);
            if (hitInfo.transform.TryGetComponent(out LaserMirror laserMirror) == false)
                return;
            Vector3 nextDirection = laserMirror.GetReflectionDirection(hitInfo.point, _stringBuilder);
            _points[1] = hitInfo.point;
            _points.Add(hitInfo.point + nextDirection);
            for (int i = 0; i < 10; i++)
            {
                Vector3 origin = Vector3.Lerp(hitInfo.point, hitInfo.point + nextDirection, 0.5f);
                ray = new Ray(origin, nextDirection);
                Physics.Raycast(ray, out hitInfo, 10f, _layerMask);
                if (hitInfo.transform.TryGetComponent(out laserMirror) == false)
                    break;
                nextDirection = laserMirror.GetReflectionDirection(hitInfo.point, _stringBuilder);
                _points[i + 2] = hitInfo.point;
                _points.Add(hitInfo.point + nextDirection);
            }
            _lineRenderer.positionCount = _points.Count;
            _lineRenderer.SetPositions(_points.ToArray());
            if (hitInfo.transform.gameObject == _target)
            {
                _actionsOnComplete?.Invoke();
                Destroy(this);
            }
        }
    }
}
