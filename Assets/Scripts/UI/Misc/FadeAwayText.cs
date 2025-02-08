using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayText : PooledPrefab<FadeAwayText>
{
    [SerializeField] TMP_Text m_tmp_text;
    public TMP_Text tmp_text => m_tmp_text;
    public void Set(float fadeTime)
    {
        float counter = 0.0f;
        DOTween.To(() => counter, value => counter = value, 1.0f, fadeTime).OnUpdate(() =>
        {
            tmp_text.color = new Color(tmp_text.color.r, tmp_text.color.g, tmp_text.color.b, 1.0f - counter);
        }).OnComplete(() =>
        {
            Release();
        });
    }
}