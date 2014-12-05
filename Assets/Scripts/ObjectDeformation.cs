using UnityEngine;
using System.Collections;

public class ObjectDeformation// : MonoBehaviour
{

    public void DeformObject(Collision obj)
    {
        //Debug.Log("Deformuojamas objektas" + obj.contacts.Length);
        MeshFilter mf = obj.collider.GetComponent<MeshFilter>();
        ///MeshFilter mf = obj.contacts[0].thisCollider.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        Vector3[] vertices = mesh.vertices;
        //Collider test =new Collider;
        
        //Debug.Log("Viršūnoų skaičius: " +vertices.Length);

        
        //Collider coll = obj.collider;
        //Vector3[] vertices = new Vector3[obj.contacts.Length];
        //for (int i=0; i<obj.contacts.Length; i++)
        //{
        //    vertices[i] = obj.contacts[i].point;
        //}
        //Debug.Log("VEktorius: " + obj.relativeVelocity.ToString());
        int p = 1;
        while (p < vertices.Length)
        {
            //for (int i = 0; i < obj.contacts.Length; i++)
            //{
                //if (vertices[p] == obj.contacts[i].point)
                //{
                    //Debug.Log("Sutampa vektoriaus koordintatės");
            //Vector3[] tempVertices = new Vector3[vertices.Length];
            //for (int i = 0; i < vertices.Length; i++ )
            //{
            //    tempVertices[i] = vertices[i];
            //}
            //vertices = tempVertices;
            //vertices[vertices.Length - 1] = obj.relativeVelocity;
            //vertices[p] += obj.relativeVelocity;
            vertices[p] += new Vector3(Random.Range(-0.3F, 0.3F), 0, Random.Range(-0.3F, 0.3F));
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

        //Colliderio pertvarkymas pagal mesh formą. 
        //obj.collider.GetComponent<MeshCollider>().sharedMesh = null;
        //obj.collider.GetComponent<MeshCollider>().sharedMesh = mesh; 
    }

    public void DeformCar(GameObject gameObject, Collision obj)
    {
        //Debug.Log("Deformuojamas automoiblis");
        //gameObject.GetComponents<Mesh>("Body");
        //gameObject.transform.DetachChildren();
        //Destroy(gameObject.);
        //MeshFilter mf = other.collider.GetComponent<MeshFilter>();
        //Mesh mesh = mf.mesh;
        //Mesh mesh = gameObject.GetComponentInChildren<MeshFilter>().mesh;
        MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();
        //for (int z = 0; z < obj.contacts.Length; z++)
        {
            //MeshFilter[] mf = obj.contacts[z].thisCollider.GetComponents<MeshFilter>();

            for (int i = 0; i < mf.Length; i++)
            {
                bool o = true;
                for (int j = 0; j < obj.contacts.Length; j++)
                {
                    if (mf[i].mesh.bounds.Contains(obj.contacts[j].point))
                    {
                        Debug.Log("Boo");
                        o = false;
                    }
                }

                if (mf[i].name != "Wheel" || !o)
                {
                    Mesh mesh = mf[i].mesh;
                    Vector3[] vertices = mesh.vertices;
                    int p = 0;
                    while (p < vertices.Length)
                    {
                        //vertices[p] += new Vector3(Random.Range(-0.3F, 0.3F), 0, Random.Range(-0.3F, 0.3F));
                        vertices[p] += new Vector3(0, 0, Random.Range(-0.01F, 0.3F));
                        p++;
                    }
                    mesh.vertices = vertices;
                    mesh.RecalculateNormals();
                    //obj.contacts[0].thisCollider.GetComponent<MeshCollider>().sharedMesh = null;
                    //obj.contacts[0].thisCollider.GetComponent<MeshCollider>().sharedMesh = mesh;
                }
            }
        }
    }
}
