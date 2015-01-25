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

    private InputSystem inputs;
    static Car car;
    static float currentSpeed;
    static float maxSpeed = 250F;

    Car()
    {
        inputs = new InputSystem();
    }

    public static Car getInstance()
    {
        if (car == null)
        {
            car = new Car();
        }
        return car;
    }


    internal float getMaxSpeed()
    {
        return maxSpeed;
    }

    internal float getCurrentSpeed()
    {
        return currentSpeed;
    }

    // Funkcija grąžina automobilio greitį, paskaičiuodama pagal rato sukimosi greitį ir rato dydį.
    // Parametrai:
    //              wheelRadius - rato diametras;
    //              wheelSpeed  - rato apsisukimai per minutę;
    // Grąžina:
    //              float tipas;
    //              Funkcija grąžina apskčiuotą automiblio greitį.
    public float getCarSpeed(float wheelRadius, float wheelSpeed)
    {
        float speed = 2 * 22 / 7 * wheelRadius * wheelSpeed * 60 / 1000;
        speed = Mathf.Round(speed);
        currentSpeed = speed;
        return speed;
    }


    // Funkcja grąžina rato sukimosi greitį.
    // Parametrai:
    //              wheelSpeed - rato apsisukimai per minutę;
    //              deltaTime  - laikas per kurį buvo pakeistas kadras;
    // Grąžnina:
    //              float tipas;
    //              Funkcija grąžina apskaičiuotą automobilio rato sukimosi greitį.
    public float getWheelRotationSpeed(float wheelSpeed, float deltaTime)
    {
        float rotation = wheelSpeed / 60 * 360 * deltaTime;
        return rotation;
    }

    // Funkcija grąžina ar nuspaustas rankinio stabdžio mygtukas.
    // Grąžina:
    //              bool tipas;
    //              Funkcija grąžina ar naudojamas rankinis stabdis.
    public bool getHandBrake()
    {
        if (inputs.getHandbrake())
        {
            return true;
        }
        return false;
    }

    // Funkcija patikrina atsižveldma į automobilio važiavimo kryptį ar automoiblis stabdo.
    // Parametrai:
    //              carSpeed - automobilio greitis.
    // Grąžina:
    //              bool tipas;
    //              Funkcija grąžina ar automiblis stabdo.
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

    // Funkcija patikrina ar autombilis išvažiavo iš žemėlapio ribų.
    // Parametrai:
    //              carPosition - dabartinė automobilio pozicija;
    //              terrainSize - žemėlapio dydis
    internal bool checkIfCarOutsideTerrain(Vector3 carPosition, Vector3 terrainSize)
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
    
