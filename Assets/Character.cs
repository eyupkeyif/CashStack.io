using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int capacity;
    Collect characterCollect;
    private void Awake()
    {
        Debug.Log("awake");

        characterCollect = GetComponent<Collect>();
        // onMoneyCollect+= ViewController.instance.UpdateProgressBar;
        //onMoneyCollect += ViewController.instance.UpdateComboBar;
        //Debug.Log("Awake");
        characterCollect.onMoneyCollect += ViewController.instance.UpdateComboBar;
        characterCollect.onMoneyCollect += ViewController.instance.UpdateCurrency;
        characterCollect.onMoneyDeliver += ViewController.instance.UpdateProgressBar;
        //capacity = 10;

        if (!PlayerPrefs.HasKey("Capacity"))
        {
            PlayerPrefs.SetInt("Capacity", characterCollect.capacity);
        }
        else
        {
            characterCollect.capacity = PlayerPrefs.GetInt("Capacity");
        }

    }


    private void OnEnable()
    {
        Debug.Log("onenable");

        characterCollect.onMoneyCollect += ViewController.instance.UpdateComboBar;
        characterCollect.onMoneyCollect += ViewController.instance.UpdateCurrency;
        characterCollect.onMoneyDeliver += ViewController.instance.UpdateProgressBar;
    }


    //public void UpgradeCapacity()
    //{
    //    capacity++;
    //    PlayerPrefs.SetInt("Capacity", capacity);
    //}
}
