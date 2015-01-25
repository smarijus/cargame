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
        //Debug.Log(string.Format("Susidurimas su :{0}", obj.gameObject.name));
        //List<string> filter1 = new List<string>(new string[] { "Terrain", "Wheel" });
        //if (deformation == null)
        //{
        //    deformation = new ObjectDeformation(this.gameObject);
        //}

        
        //List<DeformationData> impactList = recalculatePartsList(obj, new List<DeformationData>());
        //foreach (DeformationData deformationData in impactList)
        //{
        //    if(!filter1.Contains(deformationData.partName))
        //    {
        //        deformation.deformObject(deformationData.partName, deformationData.impactPoint, obj.relativeVelocity, gameObject);
        //        deformation.updateObjectMesh(gameObject, deformationData.partName);
                    
        //    }
        //}


        //int a = 3;
        //if (2 < a)
        //{
        //    return;
        //}

        DeformationData[] collisionArray = recalculatePartsList(obj);
       

        if (true)
        {



            //for (int i = 0; i < collisionArray.Length; i++)
            //{
            //    if (!collisionArray[i].partName.Contains("Wheel"))
            //    {
            //        //Debug.Log(collisionArray[i].partName);
            //        deformation.deformObject(collisionArray[i].partName, collisionArray[i].impactPoint, obj.relativeVelocity, gameObject);
            //        deformation.updateObjectMesh(gameObject, collisionArray[i].partName);
            //    }
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
                            //Debug.Log(obj.contacts[i].thisCollider.name);
                            //Debug.Log("Smūgio kažkas: " + obj.contacts[i].normal);
                            //Debug.Log("Smūgio greitis: " + Vector3.Angle(obj.relativeVelocity, obj.contacts[i].point));
                            //Debug.Log("Kampas: " + obj.contacts[i].thisCollider.transform.eulerAngles);
                            //Debug.Log(obj.contacts[i].thisCollider.transform.InverseTransformDirection(obj.relativeVelocity).normalized);
                            deformation.deformObject(obj.contacts[i].thisCollider.name, obj.contacts[i].point, obj.relativeVelocity, gameObject);
                            // deformation.updateObjectMesh(gameObject, obj.contacts[i].thisCollider.name);
                        }
                        else
                        {
                            //Debug.Log(obj.contacts[i].otherCollider.name);
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

    List<DeformationData> recalculatePartsList(Collision obj, List<DeformationData> default_value){
        List<string> containsParts       = new List<string>();
        List<ContactPoint> collisionData = new List<ContactPoint>(obj.contacts);

        foreach (ContactPoint contactPoint in collisionData)
        {
            if(contactPoint.thisCollider.transform.IsChildOf(gameObject.transform))
            {
                if (!containsParts.Contains(contactPoint.thisCollider.name))
                {
                    DeformationData deformationData = new DeformationData();
                    deformationData.partName        = contactPoint.thisCollider.name;
                    deformationData.impactPoint     = contactPoint.point;
                    default_value.Add(deformationData);
                    containsParts.Add(deformationData.partName);
                    
                }
            }
        }
        return default_value;
    }

    private int getFrequency(string name, List<ContactPoint> list)
    {
        int count = 0;
        foreach (ContactPoint item in list)
        {
            if (item.thisCollider.name.Equals(name))
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Funkcija suformuoja objekto paveiktas dalis
    /// </summary>
    /// <param name="obj">Susidurimo objektas</param>
    /// <returns>Susidurimo kontaktuojancios dalys ir kontatuojanti vieta</returns>

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
