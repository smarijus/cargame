using UnityEngine;
using System.Collections;

public class DestroyTimerScript : MonoBehaviour
{
	public float destroyAfter = 2;
	private float timer;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (destroyAfter <= timer)
		{
			Destroy(gameObject);
		}
	}
}
