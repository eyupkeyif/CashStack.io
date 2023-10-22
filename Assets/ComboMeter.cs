using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ComboMeter : MonoBehaviour
{
    [SerializeField] Slider comboBar;
    [SerializeField] TextMeshProUGUI comboText;
    float time = 1;

    public void FillComboBar(float amount)
    {
        StartCoroutine(FillComboBarCoroutine(amount));
    }

    IEnumerator FillComboBarCoroutine(float amount)
    {
        float timer = 0;

        while (timer <= time)
        {

            if (comboBar && gameObject.activeInHierarchy)
            {
                comboBar.value = Mathf.Lerp(comboBar.value, amount, Mathf.Pow(timer / time, 2));
                timer += Time.fixedDeltaTime;
                //Debug.Log("filling" + comboBar.value);
                yield return new WaitForFixedUpdate();
            }
        }

    }

    public void SetComboBarText(string _comboText)
    {
        comboText.text = _comboText;
    }

    public void DirectlyFillProgressBar(float amount)
    {

        comboBar.value = amount;
    }
    public float GetComboBarValue()
    {
        return comboBar.value;
    }

}
