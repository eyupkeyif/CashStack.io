using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
public class InGameView : View
{
    [SerializeField] TextMeshProUGUI moneyCount;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] ComboMeter comboBar;
    [SerializeField] MMF_Player currencyFeel,comboFeel;
    public override void Initialize()
    {
        ResetProgressBarAmount();
        ResetComboBarAmount();
    }

    private void OnEnable()
    {
        ResetProgressBarAmount();
        ResetComboBarAmount();
    }

    public void ResetProgressBarAmount()
    {

        progressBar.DirectlyFillProgressBar(0);
    }
    public void ResetComboBarAmount()
    {
        comboBar.DirectlyFillProgressBar(0);
    }


    public void SetProgressBarAmount(float amount)
    {
        if (progressBar.gameObject.activeInHierarchy)
        {
            progressBar.FillProgressBar(amount);
        }
    }

    public void SetComboBarAmount(float amount)
    {
        if (comboBar.gameObject.activeInHierarchy)
        {
            Debug.Log("fill bar: " + amount);
            comboBar.FillComboBar(amount);
            comboFeel?.PlayFeedbacks();
        }
    }

    public void SetProgressBarText(int count, int maxCount)
    {

        if (progressBar)
        {
            progressBar.SetProgressText(count, maxCount);
        }
    }
    public void SetMoneyCountTextBar(int moneyCounter)
    {
        moneyCount.text = moneyCounter.ToString();
        currencyFeel?.PlayFeedbacks();
    }

    public void SetComboMeterText(string comboText)
    {
        comboBar.SetComboBarText(comboText);
    }

    public float GetComboBarValues()
    {
        return comboBar.GetComboBarValue();
    }

    public void DisableProgressBar()
    {

        progressBar.gameObject.SetActive(false);
    }
    public void EnableProgressBar()
    {
        progressBar.gameObject.SetActive(true);
    }

    public void DisableComboBar()
    {
        comboBar.gameObject.SetActive(false);
    }

    public void EnableComboBar()
    {
        comboBar.gameObject.SetActive(true);
    }
}
