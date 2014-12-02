#pragma strict
var wheelFL : WheelCollider;
var wheelFR : WheelCollider;
var wheelRL : WheelCollider;
var wheelRR : WheelCollider;
var wheelFLTrans : Transform;
var wheelFRTrans : Transform;
var wheelRLTrans : Transform;
var wheelRRTrans : Transform;
var maxTorque :  float = 50;
var lowestSteerAtSpeed : float = 50;
var lowSpeedSteerAngle : float = 60;
var highSpeedSteerAngle : float = 1;
var deaccelerationSpeed : float = 30;

var currentSpeed : float;
var topSpeed : float = 250;
var topReverseSpeed : float = 50;

var braked : boolean = false;
var maxBrakeTorque : float = 100;

private var mySidewayFriction : float;
private var myForwardFriction : float;
private var slipSidewayFriction : float;
private var slipForwardFriction : float;

var speedOMeterDial : Texture2D;
var speedOMeterPointer : Texture2D;

var gearRatio : int[];
//var gear : int;

function Start ()
{
	rigidbody.centerOfMass.y = -0.9;
	rigidbody.centerOfMass.z = 0.5;
	SetValues();
}

function FixedUpdate ()
{
	Control();
	HandBrake();
}
function Update()
{
	wheelFLTrans.Rotate(wheelFL.rpm/60*360*Time.deltaTime,0,0);
	wheelFRTrans.Rotate(wheelFR.rpm/60*360*Time.deltaTime,0,0);
	wheelRLTrans.Rotate(wheelRL.rpm/60*360*Time.deltaTime,0,0);
	wheelRRTrans.Rotate(wheelRR.rpm/60*360*Time.deltaTime,0,0);
	wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
	wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
	WheelPosition();
	EngineSound();
}

function OnGUI()
{
	GUI.Label(Rect(0,Screen.height-50, 200,100), "Automobio greitis: "+currentSpeed.ToString());
}

function Control()
{
	currentSpeed = 2*22/7*wheelRL.radius*wheelRL.rpm*60/1000;
	currentSpeed = Mathf.Round(currentSpeed);
	if (currentSpeed < topSpeed && currentSpeed > -topReverseSpeed && !braked)
	{
		wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
		wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
	}
	else
	{
		wheelRR.motorTorque = 0;
		wheelRL.motorTorque = 0;
	}
	if (Input.GetButton("Vertical") == false)
	{
		wheelRR.brakeTorque = deaccelerationSpeed;
		wheelRL.brakeTorque = deaccelerationSpeed;
	}
	else
	{
		wheelRR.brakeTorque = 0;
		wheelRL.brakeTorque = 0;
	}
	var speedFactor = rigidbody.velocity.magnitude / lowestSteerAtSpeed;
	var currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
	currentSteerAngle *= Input.GetAxis("Horizontal");
	wheelFL.steerAngle = currentSteerAngle;
	wheelFR.steerAngle = currentSteerAngle;
}

function WheelPosition()
{
	var hit : RaycastHit;
	var wheelPosition : Vector3;
	if (Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, hit, wheelFL.radius + wheelFL.suspensionDistance))
	{
		wheelPosition = hit.point + wheelFL.transform.up * wheelFL.radius;
	}
	else
	{
		wheelPosition = wheelFL.transform.position -wheelFL.transform.up* wheelFL.suspensionDistance;
	}
	wheelFLTrans.position = wheelPosition;
	
	if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, hit, wheelFR.radius + wheelFR.suspensionDistance))
	{
		wheelPosition = hit.point + wheelFR.transform.up * wheelFR.radius;
	}
	else
	{
		wheelPosition = wheelFR.transform.position -wheelFR.transform.up* wheelFR.suspensionDistance;
	}
	wheelFRTrans.position = wheelPosition;
	
	if (Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, hit, wheelRL.radius + wheelRL.suspensionDistance))
	{
		wheelPosition = hit.point + wheelRL.transform.up * wheelRL.radius;
	}
	else
	{
		wheelPosition = wheelRL.transform.position -wheelRL.transform.up* wheelRL.suspensionDistance;
	}
	wheelRLTrans.position = wheelPosition;
	
	if (Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, hit, wheelRR.radius + wheelRR.suspensionDistance))
	{
		wheelPosition = hit.point + wheelRR.transform.up * wheelRR.radius;
	}
	else
	{
		wheelPosition = wheelRR.transform.position -wheelRR.transform.up* wheelRR.suspensionDistance;
	}
	wheelRRTrans.position = wheelPosition;
}	

function HandBrake()
{
	if (Input.GetKey(KeyCode.Space))
	{
		braked = true;
	}
	else
	{
		braked = false;
	}
	
	if (braked)
	{
		wheelFR.brakeTorque = maxBrakeTorque;
		wheelFL.brakeTorque = maxBrakeTorque;
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
	else
	{
		SetSlip(myForwardFriction, mySidewayFriction);
	}
}

function SetValues()
{
	myForwardFriction = wheelRR.forwardFriction.stiffness;
	mySidewayFriction = wheelRR.sidewaysFriction.stiffness;
	slipForwardFriction = 0.04;
	slipSidewayFriction = 0.08;
}

function SetSlip(currentForwardFriction : float, currentSidewayFriction : float)
{
	wheelRR.forwardFriction.stiffness = currentForwardFriction;
	wheelRL.forwardFriction.stiffness = currentForwardFriction;
	//wheelFR.forwardFriction.stiffness = currentForwardFriction;
	//wheelFL.forwardFriction.stiffness = currentForwardFriction;
	
	wheelRR.sidewaysFriction.stiffness = currentSidewayFriction;
	wheelRL.sidewaysFriction.stiffness = currentSidewayFriction;
	//wheelFR.sidewaysFriction.stiffness = currentSidewayFriction;
	//wheelFL.sidewaysFriction.stiffness = currentSidewayFriction;
}

function EngineSound()
{
	if (currentSpeed >= 0)
	{
		for (var i = 0; i < gearRatio.Length; i++)
		{
			if (gearRatio[i] > currentSpeed)
			{
				//gear = i;
				break;
			}
		}
		var gearMinValue : float = 0.00;
		var gearMaxValue : float = 0.00;
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
		var enginePitch : float = ((currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue)) + 1;
	}
	else
	{
		enginePitch = Mathf.Abs(currentSpeed / 100) + 1;
	}
	audio.pitch = enginePitch;
}