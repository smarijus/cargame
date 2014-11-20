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
var lowSpeedSteerAngle : float = 10;
var highSpeedSteerAngle : float = 1;
var deaccelerationSpeed : float = 30;

var currentSpeed : float;
var topSpeed : float = 150;
var topReverseSpeed : float = 50;

function Start ()
{
	rigidbody.centerOfMass.y = -0.9;
}

function FixedUpdate ()
{
	Control();
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
}

function OnGUI()
{
	GUI.Label(Rect(0,Screen.height-50, 200,100), "Automobio greitis: "+currentSpeed.ToString());
}

function Control()
{
	currentSpeed = 2*22/7*wheelRL.radius*wheelRL.rpm*60/1000;
	currentSpeed = Mathf.Round(currentSpeed);
	if (currentSpeed < topSpeed && currentSpeed > -topReverseSpeed)
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
	
	if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, hit, wheelFR.radius + wheelFR.suspensionDistance))
	{
		wheelPosition = hit.point + wheelFR.transform.up * wheelFR.radius;
	}
	else
	{
		wheelPosition = wheelFR.transform.position -wheelFR.transform.up* wheelFR.suspensionDistance;
	}
	wheelFRTrans.position = wheelPosition;
	
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