using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ScreenFader : UnitySingletonPersistent<ScreenFader>
{
    public Image[] solidColor;
    private Tween tween;

    private void Start()
    {
        ScreenFader.Instance.FadeOut(Color.black, GameManager.Instance._GameData.TimeFadeInOutLoading.x);
    }

    public void FadeIn(Color _color, float _time, float _delay = -1, Action _action = null, Ease _ease = Ease.InOutCubic, bool _blockRaycasts = true)
    {
        FadeTo(_color, _time, 1, _action, _ease, _delay, _blockRaycasts: _blockRaycasts);
    }

    public void FadeOut(Color _color, float _time, float _delay = -1, Action _action = null, Ease _ease = Ease.InOutCubic, bool _blockRaycasts = true)
    {
        FadeTo(_color, _time, 0, _action, _ease, _delay, _blockRaycasts: _blockRaycasts);
    }

    private void FadeTo(Color _color, float _time, float _alpha, Action _action = null, Ease _ease = Ease.InOutCubic, float _delay = -1, float _minScale = 1, float _maxScale = 1, bool _blockRaycasts = false)
    {
        FadeTo(
            _color,
            _time,
            _alpha,
            _action,
            solidColor,
            _ease,
            _delay,
            _minScale,
            _maxScale,
            _blockRaycasts
        );
    }
    private void FadeTo(Color _color, float _time, float _alpha, Action _action = null, Image _image = null, Ease _ease = Ease.InOutCubic, float _delay = -1, float _minScale = 1, float _maxScale = 1, bool _blockRaycasts = false)
    {
        FadeTo(
            _color,
            _time,
            _alpha,
            _action,
            new Image[] { _image },
            _ease,
            _delay,
            _minScale,
            _maxScale,
            _blockRaycasts
        );
    }

    private void FadeTo(Color _color, float _time, float _alpha, Action _action = null, Image[] _image = null, Ease _ease = Ease.InOutCubic, float _delay = -1, float _minScale = 1, float _maxScale = 1, bool _blockRaycasts = false)
    {
        if (_delay < 0 && tween != null && tween.active)
        {
            tween.Kill(true);
        }
        int flipx = UnityEngine.Random.value > 0.5 ? -1 : 1;
        int flipy = UnityEngine.Random.value > 0.5 ? -1 : 1;
        Color color = _color;
        float alpha = 1 - _alpha;
        tween = DOTween
            .To(
                () => alpha,
                x => alpha = x,
                _alpha,
                _time
            )
            .SetDelay(Mathf.Max(0, _delay))
            .OnStart(() => {
                color.a = alpha;
                Array.ForEach(_image, img => {
                    img.color = color;
                });
            })
            .SetEase(_ease)
            .OnUpdate(() => {
                color.a = alpha;
                Array.ForEach(_image, img => img.color = color);
                Array.ForEach(_image, (img) => {
                    Vector3 one = Vector3.forward
                        + Vector3.right * flipx
                        + Vector3.up * flipy;
                    img.transform.localScale =
                        one * Mathf.Lerp(_minScale, _maxScale, alpha);
                });
            })
            .OnComplete(
                () => {
                    if (_blockRaycasts) Array.ForEach(_image, img => img.raycastTarget = color.a == 1.0f);
                    _action?.Invoke();
                }
            );
    }
}