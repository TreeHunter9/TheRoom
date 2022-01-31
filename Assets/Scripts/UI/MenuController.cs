using System;
using UnityEngine;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        public static event Action onMenuEnable;
        public static event Action onMenuDisable;

        private void Awake()
        {
            _menu.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _menu.SetActive(!_menu.activeSelf);
                RaiseMenuEvent(_menu.activeSelf);
            }
        }

        private void RaiseMenuEvent(bool active)
        {
            if (active == true)
                onMenuEnable?.Invoke();
            else
                onMenuDisable?.Invoke();
        }
    }
}
