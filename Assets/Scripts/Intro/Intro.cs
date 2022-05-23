using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheRoom.Intro
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private AnimationCurve _imageAlphaChange;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        [SerializeField] private AnimationCurve _panelAlphaChange;
        [SerializeField] private int _sceneId;

        private void Awake()
        {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            float alpha = 0f;
            float t = 0f;
            yield return new WaitForSeconds(1f);
            while (alpha < 1f)
            {
                t += Time.deltaTime / 2f;
                alpha = _imageAlphaChange.Evaluate(t);
                _image.color = new Color(1, 1, 1, alpha);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime / 2f;
                _image.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            AsyncOperation levelScene = SceneManager.LoadSceneAsync(_sceneId, LoadSceneMode.Additive);
            levelScene.completed += operation => SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            levelScene.allowSceneActivation = false;
            
            alpha = 0f;
            StartCoroutine(LoadingTextAnimation());
            while (alpha < 1f && levelScene.progress >= 0.9f)
            {
                alpha += Time.deltaTime / 2f;
                _loadingText.color = new Color(1, 1, 1, alpha);
                yield return null;
            }
            
            yield return new WaitUntil(() => levelScene.progress < 0.9f);
            levelScene.allowSceneActivation = true;

            alpha = 1f;
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime / 2f;
                _panelCanvasGroup.alpha = _panelAlphaChange.Evaluate(alpha);
                yield return null;
            }
            
            SceneManager.UnloadSceneAsync(0);
        }

        private IEnumerator LoadingTextAnimation()
        {
            WaitForSeconds waitForHalfSecond = new WaitForSeconds(1f);
            string loading = _loadingText.text;
            while (true)
            {
                _loadingText.text += ".";
                yield return waitForHalfSecond;
                _loadingText.text += ".";
                yield return waitForHalfSecond;
                _loadingText.text += ".";
                yield return waitForHalfSecond;
                _loadingText.text = loading;
            }
        }
    }
}
