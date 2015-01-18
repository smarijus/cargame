using UnityEngine;
using System.Collections;

public class InputSystem
{

    private static float AccelerometerUpdateInterval = 1.0F / 60.0F;
    private static float LowPassKernelWidthInSeconds = 1.0F;
    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
    private Vector3 lowPassValue = Vector3.zero;


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




    public void getAccelerometerAxis()
    {
        lowPassValue = Input.acceleration;
    }

    public Vector3 LowPassFilterAccelerometer()
    {
        //lowPassValue = Mathf.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
        //return lowPassValue;
        return new Vector3();
    }
}
