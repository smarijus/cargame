using UnityEngine;
using System.Collections;

public class Car

{

    // Funkcija grąžina automobilio greitį, paskaičiuodama pagal rato sukimosi greitį.
    // Parametrai (WheelCollider)
    public float getCarSpeed(WheelCollider wheel)
    {
        float speed = 2 * 22 / 7 * wheel.radius * wheel.rpm * 60 / 1000;
        speed = Mathf.Round(speed);
        return speed;
    }


    // Funkcja grąžina rato sukimosi greitį.
    // Parametrai (WheelCollider)
    public float getWheelRotationSpeed(WheelCollider wheel)
    {
        return wheel.rpm / 60 * 360 * Time.deltaTime;
    }
}
