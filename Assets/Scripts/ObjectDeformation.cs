using UnityEngine;
using System.Collections;

public class ObjectDeformation// : MonoBehaviour
{

    public void DeformObject(Collision obj, Transform transf)
    {
        //Debug.Log("Deformuojamas objektas" + obj.contacts.Length);
        MeshFilter mf = obj.collider.GetComponent<MeshFilter>();
        ///MeshFilter mf = obj.contacts[0].thisCollider.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        Vector3[] vertices = mesh.vertices;
        Debug.Log(obj.contacts.Length);

        int p = 1;
        while (p < vertices.Length)
        {
            //float distance = Vector3.Distance(vertices[p], transf.InverseTransformPoint(obj.contacts[0].point));
            float distance = Vector3.Distance(transf.InverseTransformDirection(vertices[p]), transf.InverseTransformPoint(obj.contacts[0].point));
            Debug.Log(distance);
            if (distance < 2.5F)
            {
               Vector3 tempVertice = transf.TransformDirection(vertices[p]);
                //Vector3 tempVertice = transf.InverseTransformPoint(vertices[p]);
                //tempVertice += new Vector3(Random.Range(0F, 0.3F), 0, 0);
               tempVertice += new Vector3(0, 0.3F, 0);
               // Debug.Log(tempVertice);
                //vertices[p] += new Vector3(Random.Range(0F, 0.3F), 0, 0);
                //vertices[p] += new Vector3(0, Random.Range(0F, 0.3F), 0);
                vertices[p] = transf.InverseTransformDirection(tempVertice);
                //vertices[p] = transf.TransformPoint(tempVertice);
                //vertices[p] = tempVertice;
            }
            p++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
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
                if (mf[i].name != "Wheel")
                {
                    Mesh mesh = mf[i].mesh;
                    Vector3[] vertices = mesh.vertices;
                    int p = 0;
                    while (p < vertices.Length)
                    {
                        float distance = Vector3.Distance(gameObject.transform.InverseTransformDirection(vertices[p]), gameObject.transform.InverseTransformPoint(obj.contacts[0].point));
                        Debug.Log(distance);
                        //vertices[p] += new Vector3(0, 0, Random.Range(-0.01F, 0.3F));
                        p++;
                    }
                    mesh.vertices = vertices;
                    mesh.RecalculateNormals();
                }
            }
        }
    }
}
