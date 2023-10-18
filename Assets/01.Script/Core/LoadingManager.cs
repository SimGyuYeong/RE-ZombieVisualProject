using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    [SerializeField] private GameObject loadCanvas;

    public float LoadScene(Action _action)
    {
        Sequence _seq = DOTween.Sequence();
        GameObject _loadCanvas = PoolManager.Get(loadCanvas);
        float _fadeDelay = 0.5f;

        _seq.AppendCallback(() => _loadCanvas.GetComponent<LoadingEffect>().FadeImage(1, _fadeDelay));
        _seq.AppendInterval(_fadeDelay);
        _seq.AppendCallback(() => _action?.Invoke());
        _seq.AppendInterval(3f);
        _seq.AppendCallback(() => _loadCanvas.GetComponent<LoadingEffect>().FadeImage(0, _fadeDelay));
        _seq.AppendInterval(_fadeDelay);
        _seq.AppendCallback(() => PoolManager.Release(_loadCanvas));

        return _seq.Duration();
    }
}