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

function Start ()
{
	rigidbody.centerOfMass.y = -0.9;
}

function FixedUpdate ()
{
	wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
	wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
	wheelFL.steerAngle = 20 * Input.GetAxis("Horizontal");
	wheelFR.steerAngle = 20 * Input.GetAxis("Horizontal");
}
function Update(){
	wheelFLTrans.Rotate(wheelFL.rpm/60*360*Time.deltaTime,0,0);
	wheelFRTrans.Rotate(wheelFR.rpm/60*360*Time.deltaTime,0,0);
	wheelRLTrans.Rotate(wheelRL.rpm/60*360*Time.deltaTime,0,0);
	wheelRRTrans.Rotate(wheelRR.rpm/60*360*Time.deltaTime,0,0);
}