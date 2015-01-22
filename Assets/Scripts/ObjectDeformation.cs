using UnityEngine;
using System.Collections;

public class ObjectDeformation
{
    //private 

    private struct MeshData
    {
        public string name;
        public Vector3[] vertices;
        public int[] triangles;
    }


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

    public void deformObject(string meshName, Vector3 contactPoint, Vector3 impactVelocity, GameObject gameObject)
    {
        //GameObject[] childGameObject = gameObject.transform.GetComponentInParent<GameObject>;

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
                }
            }
            for (int i = 0; i < meshes[meshID].vertices.Length; i++)
            {
                float impactDistnace = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint).normalized, meshes[meshID].vertices[i].normalized);
                //Debug.Log(impactDistnace);
                //Debug.Log(impactVelocity);
                if (impactDistnace <= 0.35F)
                {
                    //meshes[meshID].vertices[i] += ((impactVelocity / 100));
                   meshes[meshID].vertices[i] += ((trasnform.InverseTransformDirection(impactVelocity).normalized / 100));
                }
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
            meshFilter.mesh.vertices = meshes[meshID].vertices;
            //meshCollider.sharedMesh = null; ;
            //meshCollider.sharedMesh = meshFilter.mesh;
            //meshCollider.sharedMesh.RecalculateBounds();
            //meshCollider.sharedMesh.RecalculateNormals();
            //meshFilter.sharedMesh = null; ;
            //meshFilter.sharedMesh = meshFilter.mesh;
        }
    }


}
