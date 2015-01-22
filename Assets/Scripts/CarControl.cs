using UnityEngine;
using System.Collections;
using System.Threading;

public class CarControl : MonoBehaviour
{

    //public struct Wheel
    //{
    //    public WheelCollider wheelFL;
    //    public WheelCollider wheelFR;
    //    public WheelCollider wheelRL;
    //    public WheelCollider wheelRR;
    //}

    //public Wheel wheels;

	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public Transform wheelFLTrans;
	public Transform wheelFRTrans;
	public Transform wheelRLTrans;
	public Transform wheelRRTrans;
	public float maxTorque = 50F;
	public float deaccelerationSpeed = 30F;
	
	//public float currentSpeed;
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

    public Transform transf;
    private ObjectDeformation physics;

    public bool menuStatus = false;

    private Car car = new Car();
    private UserInterface ui = new UserInterface();
    private InputSystem inputs = new InputSystem();
    private Vector3 startPosition;
    private Quaternion startRotation;
    Sound sounds = new Sound();

	// Use this for initialization
	void Start ()
	{
		rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, -0.9F, 0.5F);
        //physics = new ObjectDeformation(transf);
		SetValues();
        startPosition = transf.position;
        startRotation = transf.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Control();
        Brake();

        var ground = GameObject.Find("Terrain");
        Terrain terrain = ground.GetComponent<Terrain>();
        Vector3 terrainSize = terrain.terrainData.size;
        //Debug.Log(terrainSize);
        Vector3 carPosition = transform.position;
        if (car.checkIfCarOutsideTerrain(carPosition, terrainSize) || Game.Instance.getCarResetStatus())
        {
            transf.position = startPosition;
            transf.rotation = startRotation;
            transf.rigidbody.velocity = Vector3.zero;
            Game.Instance.setCarResetStatus(false);
        }

	}

	void Update()
	{
		wheelFLTrans.Rotate(car.getWheelRotationSpeed(wheelFL.rpm, Time.deltaTime),0,0);
        wheelFRTrans.Rotate(car.getWheelRotationSpeed(wheelFR.rpm, Time.deltaTime), 0, 0);
        wheelRLTrans.Rotate(car.getWheelRotationSpeed(wheelRL.rpm, Time.deltaTime), 0, 0);
        wheelRRTrans.Rotate(car.getWheelRotationSpeed(wheelRR.rpm, Time.deltaTime), 0, 0);
		wheelFLTrans.localEulerAngles = new Vector3(wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z, wheelFLTrans.localEulerAngles.z);
		//wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
		wheelFRTrans.localEulerAngles = new Vector3(wheelRLTrans.localEulerAngles.x, wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z, wheelFRTrans.localEulerAngles.z);
		//wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
		WheelPosition();
		playEngineSound();
        

        if (inputs.getMenuButton())
        {
            if (Game.Instance.getInGameMenuStatus())
            {
                Game.Instance.setInGameMenuStatus(false);
                //menuStatus = false;
                //Time.timeScale = 1;
            }
            else
            {
                Game.Instance.setInGameMenuStatus(true);
                //menuStatus = true;
                //Time.timeScale = 0;
            }
        }
        //Debug.Log(Input.acceleration);
	}

	void OnGUI()
	{

        if (!Game.Instance.getInGameMenuStatus())
        {
            ui.showSpeed(car.getCarSpeed(wheelRL.radius, wheelRL.rpm));
            ui.showInGameControls();
        }
        if (Game.Instance.getInGameMenuStatus())
            ui.showInGameMenu();
	}

	void Control()
	{
        if (car.getCarSpeed(wheelRL.radius, wheelRL.rpm) < topSpeed && car.getCarSpeed(wheelRL.radius, wheelRL.rpm) > -topReverseSpeed && !handbraked)
		{
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.motorTorque = maxTorque * inputs.getVerticalAxisValue();
            wheelRL.motorTorque = maxTorque * inputs.getVerticalAxisValue();

		}
		else
		{
            wheelRR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            //wheelRR.brakeTorque = 0;
            //wheelRL.brakeTorque = 0;
		}
		if (inputs.getVerticalAxis() == false)
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
        wheelFL.steerAngle = car.getCurrentSteerAngle(rigidbody.velocity.magnitude);
        wheelFR.steerAngle = car.getCurrentSteerAngle(rigidbody.velocity.magnitude);
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

    //bool MainBraked()
    //{
    //    if (car.getCarSpeed(wheelRL.radius, wheelRL.rpm) > 0 && inputs.getVerticalAxisValue() < 0)
    //        return true;
    //    if (car.getCarSpeed(wheelRL.radius, wheelRL.rpm) < 0 && inputs.getVerticalAxisValue() > 0)
    //        return true;
    //    return false;
    //}

    //bool HandBraked()
    //{
    //    if (Input.GetButton("Handbrake"))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    void Brake()
    {
        if (car.getBrake(car.getCarSpeed(wheelRL.radius, wheelRL.rpm)))
        {
            MainBrake();
        }
        else if (car.getHandBrake())
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
               // wheelFR.brakeTorque = 0;
                //wheelFL.brakeTorque = 0;
                //wheelRR.brakeTorque = 0;
                //wheelRL.brakeTorque = 0;
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

    void playEngineSound()
    {
        audio.pitch = sounds.getEngineSoundPitch(gearRatio, car.getCarSpeed(wheelRL.radius, wheelRL.rpm));
    }

    //void OnCollisionEnter(Collision obj)
    //{
    //    if (obj.collider != collider && obj.contacts.Length != 0)
    //    {
    //        //for (int i = 0; i < obj.contacts.Length; i++)
    //        //{
    //            Instantiate(spark, obj.contacts[0].point, Quaternion.identity);
    //            Instantiate(collisionSound, obj.contacts[0].point, Quaternion.identity);
    //        //}
    //    }
    //    for (int i = 0; i < 1; i++)
    //        if (!obj.contacts[i].thisCollider.name.Contains("Wheel") && !obj.contacts[i].otherCollider.name.Contains("Wheel") && !obj.contacts[i].thisCollider.name.Contains("Wheel") && !obj.contacts[i].otherCollider.name.Contains("Terrain"))
    //        {
    //            if (obj.contacts[i].thisCollider.name != obj.gameObject.name)
    //            {
    //                physics.DeformCar(obj.contacts[i].thisCollider, obj);
    //                //MeshFilter mf = obj.contacts[0].thisCollider.GetComponent<MeshFilter>();
    //                ////Thread oThread = new Thread(() => physics.DeformCar(obj.contacts[0].thisCollider, obj));
    //                //Thread oThread = new Thread(() => physics.DeformCar(obj.contacts[0].thisCollider, mf, obj));
    //                //oThread.Start();
    //            }
    //            if (obj.contacts[i].otherCollider.name != obj.gameObject.name)
    //            {
    //                physics.DeformCar(obj.contacts[i].otherCollider, obj);
    //                //MeshFilter mf = obj.contacts[0].otherCollider.GetComponent<MeshFilter>();
    //                //Thread oThread = new Thread(() => physics.DeformCar(obj.contacts[0].otherCollider, mf, obj));
    //                //oThread.Start();
    //            }
    //        }
    //}

    
}
