﻿#pragma strict
var car : Transform;
var distance : float = 6.4;
var height : float = 1.4;
var rotationDamping : float = 3.0;
var heightDamping : float = 2.0;
var zoomRacio : float = 0.5;
var DefaultFOV : float = 60;
private var rotationVector : Vector3;
function Start () {

}

function LateUpdate () {
var wantedAngel = rotationVector.y;
var wantedHeight = car.position.y + height;
var myAngel = transform.eulerAngles.y;
var myHeight = transform.position.y;
myAngel = Mathf.LerpAngle(myAngel, wantedAngel, rotationDamping * Time.deltaTime);
myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
var currentRotation = Quaternion.Euler(0, myAngel,0);
transform.position = car.position;
transform.position -= currentRotation * Vector3.forward*distance;
transform.position.y = myHeight;
transform.LookAt(car);
}

function FixedUpadate(){
var localVelocity = car.InverseTransformDirection(car.rigidbody.velocity);
if(localVelocity.z < -0.5){
rotationVector.y = car.eulerAngles.y + 180;
}else{
rotationVector.y = car.eulerAngles.y;
}
var acc = car.rigidbody.velocity.magnitude;
camera.fieldOfView = DefaultFOV + acc*zoomRacio;
}