using UnityEngine;
using System.Collections;

public class InputSystem
{

    // Funkcija grąžina vertikalios ašies reikšmę.
    public float getVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    // Funkcija grąžina horizantalios ašies reikšmę.
    public float getHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
}
