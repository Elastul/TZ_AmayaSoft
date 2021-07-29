using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class OnFadeNeededText : UnityEvent<TextMeshProUGUI, int>
{
}
[System.Serializable]
public class OnFadeNeeded : UnityEvent<Image, int>
{
}
public class UI_renderer : MonoBehaviour
{
    [SerializeField] OnFadeNeededText onFadeNeededText;
    [SerializeField] OnFadeNeeded onFadeNeeded;
    [SerializeField] GameObject _restartButton;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] Image _loadingScreen;

    private void Start() 
    {
        _loadingScreen.canvasRenderer.SetAlpha(0.0f);
    }
    public void RestartButtonActiveSwitcher()
    {
        if(_restartButton.activeSelf == true)
        {
            _restartButton.SetActive(false);
        }
        else 
        {
            onFadeNeededText.Invoke(textField, 0);
            onFadeNeeded.Invoke(_loadingScreen, 1);
            _restartButton.SetActive(true);
        }
    }

    public void UpdateObjectiveText(string text, int level)
    {
        textField.SetText(text);
        
        if(level == 1)
        {
            onFadeNeededText.Invoke(textField, 1);
            onFadeNeeded.Invoke(_loadingScreen, 0);
        }
    }
}
