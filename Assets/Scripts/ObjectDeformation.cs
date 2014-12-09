using UnityEngine;
using System.Collections;
using System.Threading;

public class ObjectDeformation// : MonoBehaviour
{

    public void DeformObject(Collision obj, Transform transf)
    {
        Debug.Log(obj.relativeVelocity/5);
        MeshFilter mf = obj.collider.GetComponent<MeshFilter>();
        ///MeshFilter mf = obj.contacts[0].thisCollider.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        Vector3[] vertices = mesh.vertices;
        Debug.Log(obj.contacts.Length);

        int p = 1;
        while (p < vertices.Length)
        {
            //float distance = Vector3.Distance(vertices[p], transf.InverseTransformPoint(obj.contacts[0].point));
            float distance = Vector3.Distance(vertices[p], transf.InverseTransformPoint(obj.contacts[0].point));
            Debug.Log(distance);
            if (distance < 2.5F)
            {
               //Vector3 tempVertice = transf.TransformDirection(vertices[p]);
                //Vector3 tempVertice = transf.InverseTransformPoint(vertices[p]);
                //tempVertice += new Vector3(Random.Range(0F, 0.3F), 0, 0);
               //tempVertice += (obj.relativeVelocity/10);
               // Debug.Log(tempVertice);
                vertices[p] += (obj.relativeVelocity/5);
                //vertices[p] += new Vector3(0, Random.Range(0F, 0.3F), 0);
                //vertices[p] = transf.InverseTransformDirection(tempVertice);
                //vertices[p] = transf.TransformPoint(tempVertice);
                //vertices[p] = tempVertice;
            }
            p++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    //public void DeformCar(Collider gameObject, Collision obj)
    public void DeformCar(Collider gameObject, Collision obj)
    //public void DeformCar(object obj)
    {
        //for (int i=0; i<10000; i++)
        //{
        //    Debug.Log("Test");
        //}
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        //MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();
        //for (int z = 0; z < obj.contacts.Length; z++)
        //{
            //MeshFilter[] mf = obj.contacts[z].thisCollider.GetComponents<MeshFilter>();

            //for (int i = 0; i < mf.Length; i++)
            //{
                if (mf.name != "Wheel")
                {
                    Mesh mesh = mf.mesh;
                    Vector3[] vertices = mesh.vertices;
                    int p = 0;
                    while (p < vertices.Length)
                    {
                        float distance = Vector3.Distance(gameObject.transform.InverseTransformDirection(vertices[p]), gameObject.transform.InverseTransformPoint(obj.contacts[0].point));
                        Debug.Log(distance);

                        if (distance < 0.7F)
                        {
                            vertices[p] += (obj.relativeVelocity / 50);
                        }

                        p++;
                    }
                    mesh.vertices = vertices;
                    mesh.RecalculateNormals();
                }
            //}
        //}
    }
}
