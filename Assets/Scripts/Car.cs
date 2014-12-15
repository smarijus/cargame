using UnityEngine;
using System.Collections;

public class Car
{
    // Parametrai skirti nustatyti vairo sukimo kampą skirtingais greičiais
    // Parametras nurodo, kokiam greičiui esant yra mžiausias sukimo kampas
    private float lowestSteerAtSpeed = 50F;
    // Parametras nurodo mažo greičio sukimo kampą
	private float lowSpeedSteerAngle = 20F;
    //Parametras nurodo didelio greičio sukimo kampą
	private float highSpeedSteerAngle = 1F;


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
    public float getWheelRotationSpeed(float wheelSpeed, float deltaTime)
    {
        float rotation = wheelSpeed / 60 * 360 * deltaTime;
        return rotation;
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

    // Funkcija grąžina automobilio priekinių ratų pasukimo kampą.
    public float getCurrentSteerAngle(float magnitude)
    {
        float speedFactor = magnitude / lowestSteerAtSpeed;
        float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
        currentSteerAngle *= inputs.getHorizontalAxisValue();
        return currentSteerAngle;
    }
}
