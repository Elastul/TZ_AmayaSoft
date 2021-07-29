using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AnimationDOtween : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    private void Start() 
    {
        _text.CrossFadeAlpha(1, 1, false);
    }

    public void DoBounce(Transform _transform)
    {
        if(_transform != null)
        {
            Sequence _bounceSequence = DOTween.Sequence();
            _bounceSequence.Append(_transform.DOScale(new Vector3(0,0,0), 0f));
            _bounceSequence.Append(_transform.DOScale(new Vector3(1.2f,1.2f,1.2f), .2f));
            _bounceSequence.Append(_transform.DOScale(new Vector3(.8f,.8f,.8f), .05f));
            _bounceSequence.Append(_transform.DOScale(new Vector3(1f,1f,1f), .05f));
        }
    }

    public void FadeLoadingScreen(Image _image, int value)
    {
        //_image.DOFade(1, .5f); отказывался работать, так что было принято решение самому реализовать FadeIn/Out эффект
        _image.CrossFadeAlpha(value, 2, false);
    }

    public void FadeText(TextMeshProUGUI _text, int value)
    {
        _text = this._text;
        
        if(value == 1)
        {
            StartCoroutine("FadeIn");
        }
        else
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn() 
    {
        for (float ft = 0f; ft <= 1; ft += 0.1f) 
        {
            Color c = _text.color;
            c.a = ft;
            _text.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FadeOut() 
    {
        DOTween.KillAll();
        for (float ft = 1f; ft >= 0; ft -= 0.1f) 
        {
            Color c = _text.color;
            c.a = ft;
            _text.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void EaseInBounce(Transform _transform)
    {
        if(_transform != null)
        {
            Sequence _bounceSequence = DOTween.Sequence();
            _bounceSequence.Append(_transform.DOShakePosition(.2f, new Vector3(.5f,0,0), 100, 0, false, true).SetEase(Ease.InBounce));
            _bounceSequence.Append(_transform.DOLocalMove(new Vector3(0,0,0), .1f, false));
        }        
    }
}
