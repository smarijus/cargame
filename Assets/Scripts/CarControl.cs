using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour
{


	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public Transform wheelFLTrans;
	public Transform wheelFRTrans;
	public Transform wheelRLTrans;
	public Transform wheelRRTrans;
	public float maxTorque = 50F;
	public float lowestSteerAtSpeed = 50F;
	public float lowSpeedSteerAngle = 60F;
	public float highSpeedSteerAngle = 1F;
	public float deaccelerationSpeed = 30F;
	
	public float currentSpeed;
	public float topSpeed  = 250F;
	public float topReverseSpeed = 50F;
	
	public bool handbraked = false;
	public float maxBrakeTorque = 100;
	
	private float mySidewayFriction;
	private float myForwardFriction;
	private float slipSidewayFriction;
	private float slipForwardFriction;

	
	public int[] gearRatio;

    public GameObject spark;
    public GameObject collisionSound;

	// Use this for initialization
	void Start ()
	{
		rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, -0.9F, 0.5F);
		//rigidbody.centerOfMass.y = -0.9;
		//rigidbody.centerOfMass.z = 0.5;
		SetValues();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Control();
        Brake();
	}

	void Update()
	{
		wheelFLTrans.Rotate(wheelFL.rpm/60*360*Time.deltaTime,0,0);
		wheelFRTrans.Rotate(wheelFR.rpm/60*360*Time.deltaTime,0,0);
		wheelRLTrans.Rotate(wheelRL.rpm/60*360*Time.deltaTime,0,0);
		wheelRRTrans.Rotate(wheelRR.rpm/60*360*Time.deltaTime,0,0);
		wheelFLTrans.localEulerAngles = new Vector3(wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z, wheelFLTrans.localEulerAngles.z);
		//wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
		wheelFRTrans.localEulerAngles = new Vector3 (wheelFLTrans.localEulerAngles.x, wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z, wheelFLTrans.localEulerAngles.z);
		//wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
		WheelPosition();
		EngineSound();
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0,Screen.height-50, 200,100), "Automobio greitis: "+currentSpeed.ToString());
	}

	void Control()
	{
		currentSpeed = 2*22/7*wheelRL.radius*wheelRL.rpm*60/1000;
		currentSpeed = Mathf.Round(currentSpeed);
		if (currentSpeed < topSpeed && currentSpeed > -topReverseSpeed && !handbraked)
		{
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
			wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
			wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

		}
		else
		{
            wheelRR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            //wheelRR.brakeTorque = 0;
            //wheelRL.brakeTorque = 0;
		}
		if (Input.GetButton("Vertical") == false)
		{
			wheelRR.brakeTorque = deaccelerationSpeed;
			wheelRL.brakeTorque = deaccelerationSpeed;
		}
		else
		{
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
		}
		var speedFactor = rigidbody.velocity.magnitude / lowestSteerAtSpeed;
		var currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
		currentSteerAngle *= Input.GetAxis("Horizontal");
		wheelFL.steerAngle = currentSteerAngle;
		wheelFR.steerAngle = currentSteerAngle;
	}

	void WheelPosition()
	{
		RaycastHit hit;
		Vector3 wheelPosition;
		if (Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, out hit, wheelFL.radius + wheelFL.suspensionDistance))
		{
			wheelPosition = hit.point + wheelFL.transform.up * wheelFL.radius;
		}
		else
		{
			wheelPosition = wheelFL.transform.position -wheelFL.transform.up* wheelFL.suspensionDistance;
		}
		wheelFLTrans.position = wheelPosition;
		
		if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, out hit, wheelFR.radius + wheelFR.suspensionDistance))
		{
			wheelPosition = hit.point + wheelFR.transform.up * wheelFR.radius;
		}
		else
		{
			wheelPosition = wheelFR.transform.position -wheelFR.transform.up* wheelFR.suspensionDistance;
		}
		wheelFRTrans.position = wheelPosition;
		
		if (Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, out hit, wheelRL.radius + wheelRL.suspensionDistance))
		{
			wheelPosition = hit.point + wheelRL.transform.up * wheelRL.radius;
		}
		else
		{
			wheelPosition = wheelRL.transform.position -wheelRL.transform.up* wheelRL.suspensionDistance;
		}
		wheelRLTrans.position = wheelPosition;
		
		if (Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, out hit, wheelRR.radius + wheelRR.suspensionDistance))
		{
			wheelPosition = hit.point + wheelRR.transform.up * wheelRR.radius;
		}
		else
		{
			wheelPosition = wheelRR.transform.position -wheelRR.transform.up* wheelRR.suspensionDistance;
		}
		wheelRRTrans.position = wheelPosition;
	}

    bool MainBraked()
    {
        if (currentSpeed > 0 && Input.GetAxis("Vertical") < 0)
            return true;
        if (currentSpeed < 0 && Input.GetAxis("Vertical") > 0)
            return true;
        return false;
    }

    bool HandBraked()
    {
        if (Input.GetButton("Handbrake"))
        {
            //handbraked = true;
            return true;
        }
        //else
        //{
            //handbraked = false;
        //    return true;
        //}
        return false;
    }

    void Brake()
    {
        if (MainBraked())
        {
            MainBrake();
        }
        else if (HandBraked())
        {
            HandBrake();
        }
        else
        {
            SetSlip(myForwardFriction, mySidewayFriction);
        }
    }

    void MainBrake()
    {
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque-40;
            wheelRL.brakeTorque = maxBrakeTorque-40;

            wheelRR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            if (rigidbody.velocity.magnitude > 1)
            {
                SetSlip(1F, 1F);
            }
            else
            {
                wheelFR.brakeTorque = 0;
                wheelFL.brakeTorque = 0;
                wheelRR.brakeTorque = 0;
                wheelRL.brakeTorque = 0;
                SetSlip(1, 1);
            }
    }

	void HandBrake()
	{
        wheelRR.brakeTorque = maxBrakeTorque;
        wheelRL.brakeTorque = maxBrakeTorque;

		wheelRR.motorTorque = 0;
		wheelRL.motorTorque = 0;
		if (rigidbody.velocity.magnitude > 1)
		{
			SetSlip(slipForwardFriction, slipSidewayFriction);
		}
		else
		{
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
			SetSlip(1, 1);
		}
	}

	void SetValues()
	{
		myForwardFriction = wheelRR.forwardFriction.stiffness;
		mySidewayFriction = wheelRR.sidewaysFriction.stiffness;
		slipForwardFriction = 0.04F;
		slipSidewayFriction = 0.08F;
	}

	void SetSlip(float currentForwardFriction, float currentSidewayFriction)
	{
		WheelFrictionCurve tempForwardFriction = wheelRR.forwardFriction;
		tempForwardFriction.stiffness = currentForwardFriction;
		wheelRR.forwardFriction = tempForwardFriction;

		tempForwardFriction = wheelRL.forwardFriction;
		tempForwardFriction.stiffness = currentForwardFriction;
		wheelRL.forwardFriction  = tempForwardFriction;

		//wheelFR.forwardFriction.stiffness = currentForwardFriction;
		//wheelFL.forwardFriction.stiffness = currentForwardFriction;

		WheelFrictionCurve tempSidewayFriction = wheelRR.sidewaysFriction;
		tempSidewayFriction.stiffness = currentSidewayFriction;
		wheelRR.sidewaysFriction = tempSidewayFriction;

		tempSidewayFriction = wheelRL.sidewaysFriction;
		tempSidewayFriction.stiffness = currentSidewayFriction;
		wheelRL.sidewaysFriction = tempSidewayFriction;


		//wheelFR.sidewaysFriction.stiffness = currentSidewayFriction;
		//wheelFL.sidewaysFriction.stiffness = currentSidewayFriction;
	}

	void EngineSound()
	{
		float enginePitch;
		if (currentSpeed >= 0)
		{
			int i = 0;
			for (i = 0; i < gearRatio.Length; i++)
			{
				if (gearRatio[i] > currentSpeed)
				{
					//gear = i;
					break;
				}
			}
			float gearMinValue = 0.00F;
			float gearMaxValue = 0.00F;
			if (i == 0)
			{
				gearMinValue = 0;
				gearMaxValue = gearRatio[i];
			}
			else
			{
				gearMinValue = gearRatio[i-1];
				gearMaxValue = gearRatio[i];	
			}
			enginePitch = ((currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue)) + 1;
		}
		else
		{
			enginePitch = Mathf.Abs(currentSpeed / 100) + 1;
		}
		audio.pitch = enginePitch;
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider != collider && other.contacts.Length != 0)
        {
            for (int i = 0; i < other.contacts.Length; i++)
            {
                Instantiate(spark, other.contacts[i].point, Quaternion.identity);
                Instantiate(collisionSound, other.contacts[i].point, Quaternion.identity);
            }
        }
    }
}
