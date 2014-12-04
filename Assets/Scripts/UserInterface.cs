using UnityEngine;
using System.Collections;

public class UserInterface
{
    public void showSpeed(float speed)
    {
        GUI.Label(new Rect(0, Screen.height - 50, 200, 100), "Automobio greitis: " + speed.ToString());
    }
}
