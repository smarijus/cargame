using UnityEngine;
using System.Collections;

public class SkiddingScript : MonoBehaviour
{

	public float currentFrictionValue;
	private float skidAt = 1.5F;
	public GameObject skidSound;
	public float soundEmittion = 15;
	private float soundWait;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		WheelHit hit;
		transform.GetComponent<WheelCollider>().GetGroundHit(out hit);
		currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
		if (skidAt <= currentFrictionValue && soundWait <= 0)
		{
			Instantiate(skidSound, hit.point, Quaternion.identity);
			soundWait = 1;
		}
		soundWait -= Time.deltaTime * soundEmittion;
	}
}
