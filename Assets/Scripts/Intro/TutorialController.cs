using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace TheRoom.Intro
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private List<TipSettings> _tipsSettings;

        private bool _isShowingTip = false;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async void ShowNextTip()
        {
            if (_isShowingTip == true)
            {
                _cancellationTokenSource.Cancel();
                await UniTask.WaitUntil(() => _isShowingTip == false);
            }
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            ShowTip(_tipsSettings[0], _cancellationTokenSource.Token);
            _tipsSettings.RemoveAt(0);
        }

        private async UniTask ShowTip(TipSettings tipSettings, CancellationToken token)
        {
            token.Register(() =>
            {
                HideTipMessage();
            });
            _isShowingTip = true;
            _textMeshPro.text = tipSettings.message;

            float timeDelay = 0f;
            await UniTask.WaitUntil(() =>
            {
                timeDelay += Time.deltaTime;
                return timeDelay > tipSettings.visibleTimeDelay;
            }, PlayerLoopTiming.Update, token);
            tipSettings.actionsOnShowTip?.Invoke();
            
            float alpha = 0f;
            while (alpha <= 1f)
            {
                if (token.IsCancellationRequested == true)
                {
                    HideTipMessage();
                    return;
                }
                alpha += Time.deltaTime;
                _textMeshPro.color = new Color(1, 1, 1, alpha);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }

            float durationTime = 0f;
            await UniTask.WaitUntil(() =>
            {
                durationTime += Time.deltaTime;
                return durationTime > tipSettings.durationTime;
            }, PlayerLoopTiming.Update, token);
            
            while (alpha >= 0f)
            {
                alpha -= Time.deltaTime;
                _textMeshPro.color = new Color(1, 1, 1, alpha);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }

            tipSettings.actionsAfterShowTip?.Invoke();
            _isShowingTip = false;
        }

        private async UniTask HideTipMessage()
        {
            float alpha = _textMeshPro.color.a;
            while (alpha >= 0f)
            {
                alpha -= Time.deltaTime;
                _textMeshPro.color = new Color(1, 1, 1, alpha);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }

            _isShowingTip = false;
        }
    }
}
