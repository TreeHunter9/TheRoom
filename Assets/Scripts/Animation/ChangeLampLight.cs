using System.Collections;
using UnityEngine;

namespace TheRoom.Animation
{
    public class ChangeLampLight : MonoBehaviour
    {
        [SerializeField, ColorUsage(true, true)] private Color _emissionColor;
        private MeshRenderer _meshFilter;
        private Material _material;
        private Color _color;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshRenderer>();
            _material = _meshFilter.materials[0];
            _color = _material.color;
        }

        private void OnEnable()
        {
            StartCoroutine(ChangeColorAsync());
        }

        public IEnumerator ChangeColorAsync()
        {
            float t = 0f;
            while (true)
            {
                if (t > 1f)
                    yield break;
                _material.color = Color.Lerp(_color, Color.white, t);
                _material.SetColor("_Emission", Color.Lerp(Color.black, _emissionColor, t));
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
