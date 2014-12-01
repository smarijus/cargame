#pragma strict
var car : Transform;
var distance : float = 6.4;
var height : float = 1.4;
var rotationDamping : float = 3.0;
var heightDamping : float = 2.0;
var zoomRacio : float = 0.5;
var DefaultFOV : float = 60;
private var rotationVector : Vector3;


//Kamoros sciptą reikia taisyti
function Start ()
{
}

function LateUpdate ()
{
	var wantedAngle = rotationVector.y;
	var wantedHeight = car.position.y + height;
	var myAngle = transform.eulerAngles.y;
	var myHeight = transform.position.y;
	myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
	myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
	var currentRotation = Quaternion.Euler(0,myAngle,0);
	transform.position = car.position;
	transform.position -= currentRotation*Vector3.forward*distance;
	transform.position.y = myHeight;
	transform.LookAt(car);
}
function FixedUpdate ()
{
	var localVilocity = car.InverseTransformDirection(car.rigidbody.velocity);
	if (localVilocity.z< 0.5)
	{
		rotationVector.y = car.eulerAngles.y + 180;
		if (Input.GetButton("Backward Camera"))
		{
			rotationVector.y = car.eulerAngles.y;
		}
	}
	else
	{
		rotationVector.y = car.eulerAngles.y;
		if (Input.GetButton("Backward Camera"))
		{
			rotationVector.y = car.eulerAngles.y + 180;
		}
	}
	
	var acc = car.rigidbody.velocity.magnitude;
	camera.fieldOfView = DefaultFOV + acc*zoomRacio;
}
