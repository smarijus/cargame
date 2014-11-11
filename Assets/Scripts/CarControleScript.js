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
var highestSpeed : float = 50;
var lowSpeedSteerAngle : float = 10;
var highSpeedSteerAngle : float = 1;
var deaccelerationSpeed : float = 30;

function Start ()
{
	rigidbody.centerOfMass.y = -0.9;
}

function FixedUpdate ()
{
	Control();
}
function Update(){
	wheelFLTrans.Rotate(wheelFL.rpm/60*360*Time.deltaTime,0,0);
	wheelFRTrans.Rotate(wheelFR.rpm/60*360*Time.deltaTime,0,0);
	wheelRLTrans.Rotate(wheelRL.rpm/60*360*Time.deltaTime,0,0);
	wheelRRTrans.Rotate(wheelRR.rpm/60*360*Time.deltaTime,0,0);
	wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
	wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
}

function Control()
{
	wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
	wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
	if (Input.GetButton("Vertical") == false)
	{
		wheelRR.brakeTorque = 0;
		wheelRL.brakeTorque = 0;
	}
	else
	{
		
	}
	var speedFactor = rigidbody.velocity.magnitude / highestSpeed;
	var currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
	currentSteerAngle *= Input.GetAxis("Horizontal");
	wheelFL.steerAngle = currentSteerAngle;
	wheelFR.steerAngle = currentSteerAngle;
}