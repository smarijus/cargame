using UnityEngine;
using System.Collections;

public class Car
{
    // Parametras nurodo, kokiam greičiui esant yra mžiausias sukimo kampas
    private float lowestSteerAtSpeed = 50F;
    // Parametras nurodo mažo greičio sukimo kampą
	private float lowSpeedSteerAngle = 20F;
    //Parametras nurodo didelio greičio sukimo kampą
	private float highSpeedSteerAngle = 1F;

    private InputSystem inputs = new InputSystem();

    // Funkcija grąžina automobilio greitį, paskaičiuodama pagal rato sukimosi greitį ir rato dydį.
    // Parametrai:
    //              wheelRadius - rato diametras;
    //              wheelSpeed  - rato apsisukimai per minutę;
    public float getCarSpeed(float wheelRadius, float wheelSpeed)
    {
        float speed = 2 * 22 / 7 * wheelRadius * wheelSpeed * 60 / 1000;
        speed = Mathf.Round(speed);
        return speed;
    }


    // Funkcja grąžina rato sukimosi greitį.
    // Parametrai:
    //              wheelSpeed - rato apsisukimai per minutę;
    //              deltaTime  - laikas per kurį buvo pakeistas kadras;
    public float getWheelRotationSpeed(float wheelSpeed, float deltaTime)
    {
        float rotation = wheelSpeed / 60 * 360 * deltaTime;
        return rotation;
    }

    // Funkcija grąžina, ar naudojamas rankinis stabdis.
    public bool getHandBrake()
    {
        if (inputs.getHandbrake())
        {
            return true;
        }
        return false;
    }

    // Funkcija grąžina ar automobilis stabdomas
    // Parametrai:
    //              carSpeed - automobilio greitis.
    public bool getBrake(float carSpeed)
    {
        if (carSpeed > 0 && inputs.getVerticalAxisValue() < 0)
            return true;
        if (carSpeed < 0 && inputs.getVerticalAxisValue() > 0)
            return true;
        return false;
    }


    // Funkcija grąžina automobilio priekinių ratų pasukimo kampą.
    // Parametrai:
    //              magnitude - objekto greičio vektoriaus ilgis.
    public float getCurrentSteerAngle(float magnitude)
    {
        float speedFactor = magnitude / lowestSteerAtSpeed;
        float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
        currentSteerAngle *= inputs.getHorizontalAxisValue();
        return currentSteerAngle;
    }

    public bool checkIfCarOutsideTerrain(Vector3 carPosition, Vector3 terrainSize)
    {
        if (carPosition.x < -50 || carPosition.x > terrainSize.x + 50)
            return true;
        if (carPosition.y < -50 || carPosition.y > terrainSize.y + 50)
            return true;
        if (carPosition.z < -50 || carPosition.z > terrainSize.z + 50)
            return true;
        return false;
    }
}
