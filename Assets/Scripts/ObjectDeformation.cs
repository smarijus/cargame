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
            float distance = Vector3.Distance(vertices[p], transf.InverseTransformPoint(obj.contacts[0].point));
            //Debug.Log(distance);
            if (distance < 2.5F)
            {
                //Vector3 tempVertice = transf.TransformPoint(vertices[p]);
                Vector3 tempVertice = transf.InverseTransformPoint(vertices[p]);
                tempVertice += new Vector3(Random.Range(0F, 0.3F), 0, 0);
               // Debug.Log(tempVertice);
                //vertices[p] += new Vector3(Random.Range(0F, 0.3F), 0, 0);
                //vertices[p] = transf.InverseTransformPoint(tempVertice);
                vertices[p] = transf.TransformPoint(tempVertice);
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
                bool o = true;
                for (int j = 0; j < obj.contacts.Length; j++)
                {
                    if (mf[i].mesh.bounds.Contains(obj.contacts[j].point))
                    {
                        //Debug.Log("Boo");
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
