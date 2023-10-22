using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewCharacterView : View
{
    public delegate void ActionDelegate();
    public ActionDelegate ConfirmButtonEvent;

    [SerializeField] CustomButton confirmButton;

    [SerializeField] List<GameObject> characterImage;
    public override void Initialize()
    {
        confirmButton.onPointerUpEvent += Confirm;
    }

    public void Confirm()
    {
        ConfirmButtonEvent?.Invoke();
    }

    public void GetNewCharacter(int i)
    {
        characterImage[i].SetActive(true);
    }
    public void ResetNewCharacter()
    {
        for (int i = 0; i < characterImage.Count; i++)
        {
            characterImage[i].SetActive(false);

        }
    }
}
