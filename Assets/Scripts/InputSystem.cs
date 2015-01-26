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
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.touchCount == 0)
        {
            //Debug.Log(Input.GetAxis("Vertical"));
            return Input.GetAxis("Vertical");
        }
        else
            return getVerticalAxisTouchValue();
    }

    // Funkcija grąžina horizantalios ašies reikšmę.
    public float getHorizontalAxisValue()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.acceleration.x == 0)
        {
            //Debug.Log(Input.GetAxis("Horizontal"));
            return Input.GetAxis("Horizontal");
        }
        else
            return getHorizontalAxisAccelerometerValue();
    }

    // Funkcija patikrina ir grąžina ar kuri nors ašis paspausta
    public bool getVerticalAxis()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.touchCount == 0)
        {
            return Input.GetButton("Vertical");
        }
        else
            return getVerticalAxisTouch();
    }

    // Funkcija grąžina ar paspaustas meniu mygtukas
    public bool getMenuButton()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.touchCount == 0)
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
        else
            if (Input.GetKeyDown(KeyCode.Escape))
                return Input.GetKeyDown(KeyCode.Escape);
            else
                return getMenuButtonTouch();
    }

    // Grąžina, ar nuspaustas rankinio stabdžio mygtukas.
    public bool getHandbrake()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.touchCount == 0)
        {
            return Input.GetButton("Handbrake");
        }
        else
            return getHandbrakeTouch();
    }

    // Funkcija grąžina ar nuspaustas kameros mygtukas.
    public bool getBackwardCamera()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Input.touchCount == 0)
        {
            return Input.GetButton("Backward Camera");
        }
        else
            return getBackwardCameraButtonTouch();
    }

    // Funkcija grąžina mobilaus įrenginio akselerometro reikšmę.
    private float getHorizontalAxisAccelerometerValue()
    {
        return Input.acceleration.x;
    }


    //public void getAccelerometerAxis()
    //{
    //    lowPassValue = Input.acceleration;
    //}

    //public Vector3 LowPassFilterAccelerometer()
    //{
    //    //lowPassValue = Mathf.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
    //    //return lowPassValue;
    //    return new Vector3();
    //}

    // Funkcjija patikrina ir grąžina akseleratoriaus reikšmę iš lietimui jautraus ekrano
    private float getVerticalAxisTouchValue()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Rect brakeButton = new Rect(Screen.width / 50, Screen.height - Screen.height / 25 - Screen.height / 5, Screen.width / 10, Screen.height / 5);
                if (brakeButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return -1;
                Rect accelerationButton = new Rect((Screen.width / 10 * 9) - Screen.width / 50, (Screen.height / 3 * 2) - Screen.width / 50, Screen.width / 10, Screen.height / 3);
                if (accelerationButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return 1;
            }
        }
        return 0;
    }

    // Funkcija patirkina ir grąžina, ar paspaustas akseleratorius lietimui jautriame ekrane
    private bool getVerticalAxisTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Rect brakeButton = new Rect(Screen.width / 50, Screen.height - Screen.height / 25 - Screen.height / 5, Screen.width / 10, Screen.height / 5);
                if (brakeButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return true;
                Rect accelerationButton = new Rect((Screen.width / 10 * 9) - Screen.width / 50, (Screen.height / 3 * 2) - Screen.width / 50, Screen.width / 10, Screen.height / 3);
                if (accelerationButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return true;
            }
        }
        return false;
    }
    
    // Funkcija grąžina ar nuspaustas rankinio stabdžio mygtukas lietimui jautriame ekrane
    private bool getHandbrakeTouch()
    {
        float verticalCenter = Screen.height / 2;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Rect handbrakeButton = new Rect(Screen.width / 50, verticalCenter, Screen.width / 10, Screen.height / 5);
                if (handbrakeButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                return true;
            }
        }
        return false;
    }

    // Funkcija grąžina ar paspaustas meniu mygtukas lietimui jautriame ekrane
    private bool getMenuButtonTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Rect menuButton = new Rect(Screen.width / 50, Screen.height / 25, Screen.width / 10, Screen.height / 10);
                if (menuButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return true;
            }
        }
        return false;
    }

    // Funkcija grąžina ar paspaustas meniu mygtukas lietimui jautriame ekrane
    private bool getBackwardCameraButtonTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Rect cameraButton = new Rect((Screen.width / 10 * 9) - Screen.width / 50, Screen.height / 25, Screen.width / 10, Screen.height / 10);
                if (cameraButton.Contains(convertTouchScreenCordinatesToGUI(touch.position)))
                    return true;
            }
        }
        return false;
    }

    // Funkcija konvertuoja ekrano lietimo kooridnates į vartotojo sąsajos koordinates
    // Parametrai:
    //              touchScreenPositin - ekrano lietimo koordinatės.
    private Vector2 convertTouchScreenCordinatesToGUI(Vector2 touchScreenPosition)
    {
        Vector2 convertedPosition = touchScreenPosition;
        convertedPosition = new Vector2(convertedPosition.x, Screen.height - convertedPosition.y);
        return convertedPosition;
    }
}
