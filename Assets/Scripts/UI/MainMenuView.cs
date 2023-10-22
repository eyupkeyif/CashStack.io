using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuView : View
{
    public delegate void ActionDelegate();

    public ActionDelegate PlayButtonEvent, HapticButtonEvent;

    [SerializeField] CustomButton playButton, hapticButton;
    [SerializeField] TextMeshProUGUI level;
    private bool playActive = true;

    public override void Initialize()
    {
        playButton.onPointerUpEvent += Play;
        //hapticButton.onPointerUpEvent += HapticSettings;
    }
    public override void Show()
    {
        base.Show();
    }
    public void Play()
    {
        if (!playActive)
        {
            return;
        }
        PlayButtonEvent?.Invoke();
    }
    public void ActivatePlay()
    {

        playActive = true;
        playButton.GetComponent<CanvasGroup>().alpha = 1;

    }
    public void DeactivatePlay()
    {

        playActive = false;
        playButton.GetComponent<CanvasGroup>().alpha = 0.4f;
    }
}
