using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);

    }
    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);

    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());

    }


    public static bool Gass()
    {
        return Input.GetButton("A_Button");
    }
    
    public static bool Brake()
    {
        return Input.GetButton("B_Button");
    }
    public static bool Boost()
    {
        return Input.GetButton("Right_Bumper");
    }
    public static bool NoBoost()
    {
        return Input.GetButtonUp("Right_Bumper");
    }


}
