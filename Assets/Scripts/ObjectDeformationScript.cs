using UnityEngine;
using System.Collections;

public class ObjectDeformationScript : MonoBehaviour
{


    ObjectDeformation deformation;

    // Use this for initialization
    void Start()
    {
        deformation = new ObjectDeformation(this.gameObject);
        //Transform[] test = gameObject.GetComponentsInChildren<Transform>();
        //for (int i = 0; i<test.Length; i++)
        //    Debug.Log(test[i].name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision obj)
    {
        for (int i = 0; i < obj.contacts.Length; i++)
        {
            if (!obj.contacts[i].thisCollider.name.Contains("Terrain") && !obj.contacts[i].otherCollider.name.Contains("Terrain"))
            {
                if (!obj.contacts[i].thisCollider.name.Contains("Wheel") && !obj.contacts[i].otherCollider.name.Contains("Wheel"))
                {

                    if (obj.contacts[i].thisCollider.transform.IsChildOf(gameObject.transform))
                    {
                        //Debug.Log("Smūgio kažkas: " + obj.contacts[i].normal);
                        //Debug.Log("Smūgio greitis: " + Vector3.Angle(obj.relativeVelocity, obj.contacts[i].point));
                        //Debug.Log("Kampas: " + obj.contacts[i].thisCollider.transform.eulerAngles);
                        Debug.Log(obj.contacts[i].thisCollider.transform.InverseTransformDirection(obj.relativeVelocity).normalized);
                        deformation.deformObject(obj.contacts[i].thisCollider.name, obj.contacts[i].point, obj.relativeVelocity, gameObject);
                        deformation.updateObjectMesh(gameObject, obj.contacts[i].thisCollider.name);
                    }
                    else
                    {
                        //Debug.Log("Smūgio greitis: " + Vector3.Angle(obj.relativeVelocity, obj.contacts[i].point));
                        //Debug.Log("Smūgio kažkas: " + obj.contacts[i].normal);
                        //Debug.Log("Smūgio greitis: " + obj.relativeVelocity);
                        //Debug.Log("Kampas: " + obj.contacts[i].otherCollider.transform.eulerAngles);
                        Debug.Log(obj.contacts[i].otherCollider.transform.InverseTransformDirection(obj.relativeVelocity).normalized);
                        deformation.deformObject(obj.contacts[i].otherCollider.name, obj.contacts[i].point, obj.relativeVelocity, gameObject);
                        deformation.updateObjectMesh(gameObject, obj.contacts[i].otherCollider.name);
                    }
                }
            }
        }

    }
}
