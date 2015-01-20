using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour
{

	public Transform car;
	public float distance = 6.4F;
	public float height = 1.4F;
	public float rotationDamping = 3.0F;
	public float heightDamping = 2.0F;
	public float zoomRacio = 0.5F;
	public float DefaultFOV = 60;
	private Vector3 rotationVector;


    private InputSystem inputs = new InputSystem();
	// Use this for initialization
	void Start () {
	
	}
	
	void LateUpdate ()
	{
		float wantedAngle = rotationVector.y;
		float wantedHeight = car.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;
		myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
		var currentRotation = Quaternion.Euler(0,myAngle,0);
		transform.position = car.position;
		transform.position -= currentRotation*Vector3.forward*distance;
		transform.position = new Vector3(transform.position.x, myHeight, transform.position.z);
		transform.LookAt(car);
	}

	void FixedUpdate ()
	{
		var localVilocity = car.InverseTransformDirection(car.rigidbody.velocity);
		if (localVilocity.z< 0.5)
		{
			rotationVector.y = car.eulerAngles.y + 180;
			if (inputs.getBackwardCamera())
			{
				rotationVector.y = car.eulerAngles.y;
			}
		}
		else
		{
			rotationVector.y = car.eulerAngles.y;
			if (inputs.getBackwardCamera())
			{
				rotationVector.y = car.eulerAngles.y + 180;
			}
		}
		
		var acc = car.rigidbody.velocity.magnitude;
		camera.fieldOfView = DefaultFOV + acc*zoomRacio;
	}
}
