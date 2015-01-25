using UnityEngine;
using System.Collections;

public class ObjectDeformation
{
    //private 



    private MeshData[] meshes;
    public ObjectDeformation(GameObject gameObject)
    {
        MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();
        meshes = new MeshData[mf.Length];
        for (int i = 0; i < mf.Length; i++)
        {
            meshes[i].name = mf[i].name;
            //Debug.Log(mf[i].name);
            meshes[i].triangles = mf[i].mesh.triangles;
            meshes[i].vertices = mf[i].mesh.vertices;
        }
    }

    public MeshData[] getMeshData()
    {
        return meshes;
    }

    public void deformObject(string meshName, Vector3 contactPoint, Vector3 impactVelocity, GameObject gameObject)
    {
        //GameObject[] childGameObject = gameObject.transform.GetComponentInParent<GameObject>;
        Car car = Car.getInstance();
        float currentSpeed = car.getCurrentSpeed();
        float maxSpeed = car.getTopSpeed();
        //Debug.Log(string.Format("Deformuojamas objektas:{0} Jega:{1}", meshName, currentSpeed));

        Transform[] transformsList = gameObject.GetComponentsInChildren<Transform>();
        Transform trasnform = null;
        for (int i = 0; i<transformsList.Length; i++)
        {
            if (transformsList[i].name == meshName)
            {
                trasnform = transformsList[i];
                break;
            }
        }
        
        if (trasnform != null)
        {
            int meshID = 0;
            for (int i = 0; i < meshes.Length; i++)
            {
                if (meshes[i].name == meshName)
                {
                    meshID = i;
                    break;
                }
            }
<<<<<<< HEAD
                for (int i = 0; i < meshes[meshID].vertices.Length; i++)
                {
                    float impactDistnace = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint), meshes[meshID].vertices[i]);
                    //Debug.Log(meshName+" "+impactDistnace);
                    //Debug.Log(impactVelocity);
                    if (impactDistnace <= 0.3F)
                    {
                        //Debug.Log(impactDistnace);
                        //meshes[meshID].vertices[i] += ((impactVelocity / 100));
                        //Debug.Log(((trasnform.InverseTransformDirection(impactVelocity)))/10);
                        ///meshes[meshID].vertices[i] += ((trasnform.InverseTransformDirection(impactVelocity))/90);
                        //meshes[meshID].vertices[i] += (impactVelocity / 50);

                        float kof = Mathf.Abs((currentSpeed / maxSpeed) + 0.001F);
                       meshes[meshID].vertices[i] += (trasnform.InverseTransformDirection(impactVelocity)) * kof;
                        //meshes[meshID].vertices[i] += Vector3.up * kof;
                    }
=======
            for (int i = 0; i < meshes[meshID].vertices.Length; i++)
            {
                float impactDistnace = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint), meshes[meshID].vertices[i]);
                //Debug.Log(meshName+" "+impactDistnace);
                //Debug.Log(impactVelocity);
                if (impactDistnace <= 0.75F)
                {
                    //Debug.Log(impactDistnace);
                    //meshes[meshID].vertices[i] += ((impactVelocity / 100));
                    //Debug.Log(((trasnform.InverseTransformDirection(impactVelocity)))/10);
                    meshes[meshID].vertices[i] += ((trasnform.InverseTransformDirection(impactVelocity)) / ((maxSpeed - Mathf.Abs(currentSpeed)) + 50));
                    //meshes[meshID].vertices[i] += (impactVelocity / 50);

                    //float kof = (currentSpeed / maxSpeed) + 0.001F;
                    //meshes[meshID].vertices[i] += (trasnform.InverseTransformDirection(impactVelocity)) * kof/100;
>>>>>>> 06dad261f5a1ab23208f7b5f9b8e1544a4be6920
                }
        }
    }

    public void updateObjectMesh(GameObject gameObject, string objectName)
    {
        MeshFilter meshFilter = null;
        MeshCollider meshCollider = null;
        MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < mf.Length; i++)
        {
            if (mf[i].name == objectName)
            {
                meshFilter = mf[i];
                break;
            }
        }

        MeshCollider[] mc = gameObject.GetComponentsInChildren<MeshCollider>();
        for (int i = 0; i < mc.Length; i++)
        {
            if (mc[i].name == objectName)
            {
                meshCollider = mc[i];
                break;
            }
        }

        int meshID = -1;
        for (int i = 0; i < meshes.Length; i++)
        {
            if (meshes[i].name == objectName)
            {
                meshID = i;
                break;
            }
        }


        if (meshFilter != null && meshID != -1)
        {
            //Debug.Log("Updating mesh");
            meshFilter.mesh.vertices = meshes[meshID].vertices;
            //meshCollider.sharedMesh = null; ;
            //meshCollider.sharedMesh = meshFilter.mesh;
           meshCollider.sharedMesh.RecalculateBounds();
            //meshCollider.sharedMesh.RecalculateNormals();
        }
    }


}
