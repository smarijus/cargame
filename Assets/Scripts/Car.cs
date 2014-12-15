using UnityEngine;
using System.Collections;

public class Car

{

    private InputSystem inputs = new InputSystem();

    // Funkcija grąžina automobilio greitį, paskaičiuodama pagal rato sukimosi greitį ir rato dydį.
    // Parametrai (WheelCollider)
    public float getCarSpeed(float wheelRadius, float wheelSpeed)
    {
        float speed = 2 * 22 / 7 * wheelRadius * wheelSpeed * 60 / 1000;
        speed = Mathf.Round(speed);
        return speed;
    }


    // Funkcja grąžina rato sukimosi greitį.
    // Parametrai (WheelCollider)
    public float getWheelRotationSpeed(float wheelSpeed)
    {
        return wheelSpeed / 60 * 360 * Time.deltaTime;
    }

    // Funkcija grąžina, ar naudojamas rankinis stabdis.
    public bool HandBraked()
    {
        if (inputs.getHandbrake())
        {
            return true;
        }
        return false;
    }
}
