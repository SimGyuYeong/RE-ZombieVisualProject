using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingEffect : MonoBehaviour
{
    [SerializeField]
    private Image loadImage;

    public float FadeImage(int _opacity, float _duration)
    {
        Sequence _seq = DOTween.Sequence();

        Debug.Log("페이드 " + _opacity);
        _seq.Append(loadImage.DOFade(_opacity, _duration));
        
        return _seq.Duration();
    }
}
