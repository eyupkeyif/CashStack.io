using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeView : View
{
    public delegate void ActionDelegate();

    public ActionDelegate SpeedButtonEvent,MultiplierButtonEvent;

    [SerializeField] CustomButton speed, multiplier;

    public override void Initialize()
    {
        speed.onPointerUpEvent += SelectSpeed;
        multiplier.onPointerUpEvent += SelectMultiplier;
    }


    public void SelectSpeed()
    {
        SpeedButtonEvent?.Invoke();
    }

    public void SelectMultiplier()
    {
        MultiplierButtonEvent?.Invoke();
    }
}
