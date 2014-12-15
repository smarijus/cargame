using UnityEngine;
using System.Collections;

public class InputSystem
{

    // Funkcija grąžina vertikalios ašies reikšmę.
    public float getVerticalAxisValue()
    {
        return Input.GetAxis("Vertical");
    }

    // Funkcija grąžina horizantalios ašies reikšmę.
    public float getHorizontalAxisValue()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool getVerticalAxis()
    {
        return Input.GetButton("Vertical");
    }

    // Grąžina, ar nuspaustas rankinio stabdžio mygtukas.
    public bool getHandbrake()
    {
        return Input.GetButton("Handbrake");
    }
}
