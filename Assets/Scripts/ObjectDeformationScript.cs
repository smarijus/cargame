using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDeformationScript : MonoBehaviour
{
    private struct DeformationData
    {
        public string partName;
        public Vector3 impactPoint;
    }

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
        if (true)
        {
            DeformationData[] collisionArray = recalculatePartsList(obj);

            //for (int i = 0; i < collisionArray.Length; i++)
            //{
            //    if (!collisionArray[i].partName.Contains("Wheel"))
            //        {
            //            Debug.Log(collisionArray[i].partName);
            //            deformation.deformObject(collisionArray[i].partName, collisionArray[i].impactPoint, obj.relativeVelocity, gameObject);
            //            deformation.updateObjectMesh(gameObject, collisionArray[i].partName);
            //        }
            //}

            ////for (int i = 0; i < 1; i++)
            for (int i = 0; i < obj.contacts.Length; i++)
            {
                //Debug.Log(obj.contacts[i].thisCollider.name + " " + obj.contacts[i].otherCollider.name);
                if (!obj.contacts[i].thisCollider.name.Contains("Terrain") && !obj.contacts[i].otherCollider.name.Contains("Terrain"))
                {
                    if (!obj.contacts[i].thisCollider.name.Contains("Wheel") && !obj.contacts[i].otherCollider.name.Contains("Wheel"))
                    {

                        if (obj.contacts[i].thisCollider.transform.IsChildOf(gameObject.transform))
                        {
                            Debug.Log(obj.contacts[i].thisCollider.name);
                            //Debug.Log("Smūgio kažkas: " + obj.contacts[i].normal);
                            //Debug.Log("Smūgio greitis: " + Vector3.Angle(obj.relativeVelocity, obj.contacts[i].point));
                            //Debug.Log("Kampas: " + obj.contacts[i].thisCollider.transform.eulerAngles);
                            //Debug.Log(obj.contacts[i].thisCollider.transform.InverseTransformDirection(obj.relativeVelocity).normalized);
                            deformation.deformObject(obj.contacts[i].thisCollider.name, obj.contacts[i].point, obj.relativeVelocity, gameObject);
                           // deformation.updateObjectMesh(gameObject, obj.contacts[i].thisCollider.name);
                        }
                        else
                        {
                            Debug.Log(obj.contacts[i].otherCollider.name);
                            //Debug.Log("Smūgio greitis: " + Vector3.Angle(obj.relativeVelocity, obj.contacts[i].point));
                            //Debug.Log("Smūgio kažkas: " + obj.contacts[i].normal);
                            //Debug.Log("Smūgio greitis: " + obj.relativeVelocity);
                            //Debug.Log("Kampas: " + obj.contacts[i].otherCollider.transform.eulerAngles);
                            //Debug.Log(obj.contacts[i].otherCollider.transform.InverseTransformDirection(obj.relativeVelocity).normalized);
                            deformation.deformObject(obj.contacts[i].otherCollider.name, obj.contacts[i].point, obj.relativeVelocity, gameObject);
                            //deformation.updateObjectMesh(gameObject, obj.contacts[i].otherCollider.name);
                        }
                    }
                }
            }
            for (int i = 0; i < collisionArray.Length; i++)
            {
                deformation.updateObjectMesh(gameObject, collisionArray[i].partName);
            }
        }

    }

    DeformationData[] recalculatePartsList(Collision obj)
    {
        DeformationData[] collisionArray = new DeformationData[0];
        for (int i = 0; i < obj.contacts.Length; i++)
        {
            bool alreadyExist = false;
            for (int j = 0; j < collisionArray.Length; j++)
            {
                if (obj.contacts[i].thisCollider.transform.IsChildOf(gameObject.transform))
                {
                    if (obj.contacts[i].thisCollider.name == collisionArray[j].partName)
                    {
                        alreadyExist = true;
                    }
                }
                if (obj.contacts[i].otherCollider.transform.IsChildOf(gameObject.transform))
                {
                    if (obj.contacts[i].otherCollider.name == collisionArray[j].partName)
                    {
                        alreadyExist = true;
                    }
                }
            }
            if (alreadyExist == false)
            {
                DeformationData[] tempArray = new DeformationData[collisionArray.Length + 1];
                for (int j = 0; j < collisionArray.Length; j++)
                {
                    tempArray[j] = collisionArray[j];
                }
                if (obj.contacts[i].thisCollider.transform.IsChildOf(gameObject.transform))
                {
                    tempArray[tempArray.Length - 1].partName = obj.contacts[i].thisCollider.name;
                    tempArray[tempArray.Length - 1].impactPoint = obj.contacts[i].point;
                }
                if (obj.contacts[i].otherCollider.transform.IsChildOf(gameObject.transform))
                {
                    tempArray[tempArray.Length - 1].partName = obj.contacts[i].otherCollider.name;
                    tempArray[tempArray.Length - 1].impactPoint = obj.contacts[i].point;
                }
                collisionArray = tempArray;

            }
            alreadyExist = false;
        }
        return collisionArray;
    }
}
