using UnityEngine;
using System.Collections;

public class ObjectDeformation// : MonoBehaviour
{

    public void DeformObject(Collision obj)
    {
        Debug.Log("Deformuojamas objektas" + obj.contacts.Length);
        MeshFilter mf = obj.collider.GetComponent<MeshFilter>();
        ///MeshFilter mf = obj.contacts[0].thisCollider.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        Vector3[] vertices = mesh.vertices;

        
        //Collider coll = obj.collider;
        //Vector3[] vertices = new Vector3[obj.contacts.Length];
        //for (int i=0; i<obj.contacts.Length; i++)
        //{
        //    vertices[i] = obj.contacts[i].point;
        //}

        int p = 1;
        while (p < vertices.Length)
        {
            //for (int i = 0; i < obj.contacts.Length; i++)
            //{
                //if (vertices[p] == obj.contacts[i].point)
                //{
                    //Debug.Log("Sutampa vektoriaus koordintatės");
            vertices[p] += new Vector3(0, Random.Range(-0.3F, 0.3F), 0);
            //vertices[p] += obj.impactForceSum;
                    //vertices[p] += new Vector3(obj.contacts[0].point.x, obj.contacts[0].point.y, obj.contacts[0].point.z);
                   
                //}
            //}
            //vertices[p] = new Vector3(obj.contacts[0].point.x, obj.contacts[0].point.y, obj.contacts[0].point.z);
            p++;
        }
        //obj.contacts[i].otherCollider.
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public void DeformCar(GameObject gameObject)
    {
        Debug.Log("Deformuojamas automoiblis");
        //gameObject.GetComponents<Mesh>("Body");
        //gameObject.transform.DetachChildren();
        //Destroy(gameObject.);
        //MeshFilter mf = other.collider.GetComponent<MeshFilter>();
        //Mesh mesh = mf.mesh;
        //Mesh mesh = gameObject.GetComponentInChildren<MeshFilter>().mesh;
        MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();

        for (int i=0; i<mf.Length; i++)
        {
            Mesh mesh = mf[i].mesh;
            Vector3[] vertices = mesh.vertices;
            int p = 0;
            while (p < vertices.Length)
            {
                vertices[p] += new Vector3(0, Random.Range(-0.3F, 0.3F), 0);
                p++;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }
    }
}
