using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TheRoom.UI
{
    public class FPSCounter : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            StartCoroutine(StartCount());
        }

        private IEnumerator StartCount()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            while (true)
            {
                int fpsCount = Convert.ToInt32(1 / Time.deltaTime);
                _textMeshPro.text = fpsCount.ToString();
                yield return waitForSecond;
            }
        }
    }
}
