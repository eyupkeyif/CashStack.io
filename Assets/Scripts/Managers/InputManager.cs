using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Vector2 input = Vector2.zero;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public Vector2 GetInput()
    {
        input = new Vector2(joystick.Horizontal, joystick.Vertical);
        return input;
    }

    public void JoystickEnable()
    {
        joystick.Enable();
    }

    public void JoystickDisable()
    {
        joystick.Disable();
    }
}
