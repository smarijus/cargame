using UnityEngine;
using System.Collections;

public class ObjectDeformation
{
    private struct MeshData
    {
        public string name;
        public Vector3[] vertices;
        public int[] triangles;
        //public Vector3[][] nearVerticesCoordinates;
        public int[][] nearVerticesIndexes;
        public float damperForce;
    }

    private float gravity = 9.98F;
    private MeshData[] meshes;
    private float mass;
    ArrayList usedVerticesList = new ArrayList();

    public ObjectDeformation(GameObject gameObject)
    {
        mass = gameObject.rigidbody.mass;
        MeshFilter[] mf = gameObject.GetComponentsInChildren<MeshFilter>();
        meshes = new MeshData[mf.Length];
        for (int i = 0; i < mf.Length; i++)
        {
            meshes[i].name = mf[i].name;
            //Debug.Log(mf[i].name);
            meshes[i].vertices = mf[i].mesh.vertices;
            //Debug.Log("Viršūnių kiekis: "+meshes[i].vertices.Length);
            meshes[i].triangles = mf[i].mesh.triangles;
            meshes[i].damperForce = 1000F;
            if (Game.Instance.getAccurateDeformationModeStatus())
            {
                if (!meshes[i].name.Contains("Wheel") || !meshes[i].name.Contains("Interior"))
                {
                    Vector3[][] tempNearVerticesCoordinates = new Vector3[meshes[i].vertices.Length][];
                    int[][] tempNearVerticesIndexes = new int[meshes[i].vertices.Length][];
                    for (int j = 0; j < meshes[i].vertices.Length; j++)
                    {
                        int neighbourdsCount = 0;
                        for (int k = 0; k < meshes[i].triangles.Length; k = k + 3)
                        {
                            //int tempNumber = 0;
                            if (meshes[i].triangles[k] == j)
                            {
                                neighbourdsCount = neighbourdsCount + 2;
                            }
                            if (meshes[i].triangles[k + 1] == j)
                            {
                                neighbourdsCount = neighbourdsCount + 2;
                            }
                            if (meshes[i].triangles[k + 2] == j)
                            {
                                neighbourdsCount = neighbourdsCount + 2;
                            }
                        }


                        //Vector3[] tempArrayCoordinates = new Vector3[neighbourdsCount];
                        int[] tempArrayIndexes = new int[neighbourdsCount];
                        for (int k = 0; k < meshes[i].triangles.Length; k = k + 3)
                        {
                            int tempNumber = 0;
                            if (meshes[i].triangles[k] == j)
                            {
                                //neighbourdsCount = neighbourdsCount + 2;
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k + 1]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k + 1];
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k + 1]]);
                                tempNumber++;
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k + 2]]);
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k + 2]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k + 2];
                                tempNumber++;
                            }
                            if (meshes[i].triangles[k + 1] == j)
                            {
                                //neighbourdsCount = neighbourdsCount + 2;
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k]]);
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k];
                                tempNumber++;
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k + 2]]);
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k + 2]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k + 2];
                                tempNumber++;
                            }
                            if (meshes[i].triangles[k + 2] == j)
                            {
                                //neighbourdsCount = neighbourdsCount + 2;
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k]]);
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k];
                                tempNumber++;
                                //Debug.Log(meshes[i].vertices[meshes[i].triangles[k + 1]]);
                                //tempArrayCoordinates[tempNumber] = meshes[i].vertices[meshes[i].triangles[k + 1]];
                                tempArrayIndexes[tempNumber] = meshes[i].triangles[k + 1];
                                tempNumber++;
                            }
                        }
                        //tempNearVerticesCoordinates[j] = tempArrayCoordinates;
                        //Debug.Log("Šale esančio viršūnės:" + neighbourdsCount);
                        tempNearVerticesIndexes[j] = tempArrayIndexes;
                    }
                    //meshes[i].nearVerticesCoordinates = tempNearVerticesCoordinates;
                    meshes[i].nearVerticesIndexes = tempNearVerticesIndexes;
                }
            }
        }
    }

    public void deformObject(string meshName, Vector3 contactPoint, Vector3 impactVelocity, GameObject gameObject)
    {
        if (Game.Instance.getAccurateDeformationModeStatus())
        {
            float force = mass * impactVelocity.normalized.magnitude;
            //float fExtarnal = (mass * impactVelocity.magnitude);
            //Debug.Log(force);

            //GameObject[] childGameObject = gameObject.transform.GetComponentInParent<GameObject>;

            Transform[] transformsList = gameObject.GetComponentsInChildren<Transform>();
            Transform trasnform = null;
            for (int i = 0; i < transformsList.Length; i++)
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
                //int number = 0;
                //float minDistance = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint), meshes[meshID].vertices[0]);
                //int tempId = 0;

                for (int i = 0; i < meshes[meshID].vertices.Length; i++)
                {
                    float impactDistnace = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint), meshes[meshID].vertices[i]);
                    //Debug.Log(meshName+" "+impactDistnace);
                    //Debug.Log(impactVelocity);
                    //Debug.Log(impactDistnace);
                    //if (impactDistnace < minDistance)
                    //{
                    //    minDistance = impactDistnace;
                    //    tempId = i;
                    //}
                    if (impactDistnace <= 0.2F)
                    {
                        Vector3 normalizedImpactVelocity = trasnform.InverseTransformDirection(impactVelocity) * force / 200000;
                        //ArrayList usedVericesList = new ArrayList();
                        //for (int j = 0; j < meshes[meshID].nearVerticesIndexes[i].Length; j++)
                        //{
                        //bool used = false;
                        ////foreach (int item in usedVerticesList)
                        //{
                        //    //if (item == meshes[meshID].nearVerticesIndexes[i][j])
                        //    {
                        //        used = true;
                        //    }
                        //}
                        //if (used == false)
                        //{
                        //usedVerticesList.Add(meshes[meshID].nearVerticesIndexes[i][j]);
                        deformMesh(meshID, i, force, normalizedImpactVelocity, usedVerticesList);
                        //}
                        //used = false;
                        //}

                        //    //float[] tempArray = new float[meshes[meshID].nearVerticesCoordinates[i].Length];
                        //    ////float[] forces = new float[meshes[meshID].nearVerticesCoordinates[i].Length];
                        //    ////int tempIndex = 0;
                        //    //for (int j = 0; j < meshes[meshID].nearVerticesCoordinates[i].Length; j++)
                        //    //{
                        //    //    tempArray[j] = Vector3.Distance(meshes[meshID].vertices[i], meshes[meshID].nearVerticesCoordinates[i][j]);
                        //    //    //forces[j] = tempArray[j] * meshes[meshID].damperForce;
                        //    //}
                        //    //Vector3 normalizedImpactVelocity = Vector3.Distance(meshes[meshID].vertices[i], meshes[meshID].nearVerticesCoordinates[i][0]);
                        //    Vector3 normalizedImpactVelocity = trasnform.InverseTransformDirection(impactVelocity) / 90;
                        //    ArrayList usedVericesList = new ArrayList();
                        //    usedVericesList.Add(i);
                        //    deformMesh(meshID, i, force, normalizedImpactVelocity, usedVericesList);

                    }
                }
                //Vector3 normalizedImpactVelocity = trasnform.InverseTransformDirection(impactVelocity) / 40;
                //ArrayList usedVericesList = new ArrayList();
                //usedVericesList.Add(tempId);
                //deformMesh(meshID, tempId, force, normalizedImpactVelocity, usedVericesList);
                //Debug.Log("kiekis: " + number);
                //trasnform.rigidbody.velocity += (trasnform.InverseTransformDirection(impactVelocity)/90);
            }
        }
        else
        {
            LowDeformation(meshName, contactPoint, impactVelocity, gameObject);
        }
    }

    private void deformMesh(int meshesIndex, int verticeIndex, float force, Vector3 normalizedImpactVelocity, ArrayList usedVerticesList)
    {
        //float[] tempArray = new float[meshes[meshesIndex].nearVerticesCoordinates[verticeIndex].Length];
        //Debug.Log(force);
        for (int j = 0; j < meshes[meshesIndex].nearVerticesIndexes[verticeIndex].Length; j++)
        {
            bool used = false;
            foreach (int item in usedVerticesList)
            {
                if (item == meshes[meshesIndex].nearVerticesIndexes[verticeIndex][j])
                {
                    used = true;
                }
            }
            if (force / 1.5F > meshes[meshesIndex].damperForce)
            {
                if (used == false)
                {
                    usedVerticesList.Add(meshes[meshesIndex].nearVerticesIndexes[verticeIndex][j]);
                    deformMesh(meshesIndex, meshes[meshesIndex].nearVerticesIndexes[verticeIndex][j], force / 1.5F, normalizedImpactVelocity / 1.5F, usedVerticesList);
                }
            }
            used = false;
        }
        //float test = Vector3.Distance(meshes[meshesIndex].vertices[verticeIndex], meshes[meshesIndex].nearVerticesCoordinates[verticeIndex][0]);
        meshes[meshesIndex].vertices[verticeIndex] += normalizedImpactVelocity;
        Game.Instance.updateCurrentScore(1);
    }


    //private void deformMesh(int meshesIndex, int verticeIndex, float force, Vector3 normalizedImpactVelocity, int rekursija)
    //{
    //    float[] tempArray = new float[meshes[meshesIndex].nearVerticesCoordinates[verticeIndex].Length];
    //    float[] forces = new float[meshes[meshesIndex].nearVerticesCoordinates[verticeIndex].Length];
    //    //int tempIndex = 0;
    //    for (int j = 0; j < meshes[meshesIndex].nearVerticesCoordinates[verticeIndex].Length; j++)
    //    {
    //        float distance = Vector3.Distance(meshes[meshesIndex].vertices[verticeIndex], meshes[meshesIndex].nearVerticesCoordinates[verticeIndex][j]);
    //        //tempArray[j] = Vector3.Distance(meshes[meshesIndex].vertices[verticeIndex], meshes[meshesIndex].nearVertices[verticeIndex][j]);
    //        //forces[j] = tempArray[j] * meshes[meshesIndex].damperForce;
    //        float resistanceForce = distance * meshes[meshesIndex].damperForce;
    //        //Debug.Log("verticeIndex: " + meshes[meshesIndex].nearVerticesIndexes[verticeIndex][j]);
    //        rekursija++;
    //        if (force - resistanceForce > 0)
    //            deformMesh(meshesIndex, meshes[meshesIndex].nearVerticesIndexes[verticeIndex][j], force - resistanceForce, normalizedImpactVelocity, rekursija);
    //    }
    //    float test = Vector3.Distance(meshes[meshesIndex].vertices[verticeIndex], meshes[meshesIndex].nearVerticesCoordinates[verticeIndex][0]);
    //    meshes[meshesIndex].vertices[verticeIndex] += normalizedImpactVelocity * Vector3.Distance(meshes[meshesIndex].vertices[verticeIndex], meshes[meshesIndex].nearVerticesCoordinates[verticeIndex][0]);
    //}

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
            //meshFilter.sharedMesh = null; ;
            //meshFilter.sharedMesh = meshFilter.mesh;
            usedVerticesList.Clear();
        }
    }


    private void LowDeformation(string meshName, Vector3 contactPoint, Vector3 impactVelocity, GameObject gameObject)
    {

        //GameObject[] childGameObject = gameObject.transform.GetComponentInParent<GameObject>;
        Car car = Car.getInstance();
        float currentSpeed = car.getCurrentSpeed();
        float maxSpeed = car.getTopSpeed();
        //Debug.Log(string.Format("Deformuojamas objektas:{0} Jega:{1}", meshName, currentSpeed));
        Transform[] transformsList = gameObject.GetComponentsInChildren<Transform>();
        Transform trasnform = null;
        for (int i = 0; i < transformsList.Length; i++)
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
            float force = mass * impactVelocity.normalized.magnitude;
            Vector3 normalizedImpactVelocity = trasnform.InverseTransformDirection(impactVelocity) * force / 200000;
            for (int i = 0; i < meshes[meshID].vertices.Length; i++)
            {
                float impactDistnace = Vector3.Distance(trasnform.InverseTransformPoint(contactPoint), meshes[meshID].vertices[i]);
                //Debug.Log(meshName+" "+impactDistnace);
                //Debug.Log(impactVelocity);
                if (impactDistnace <= 0.2F)
                {
                    //Debug.Log(impactDistnace);
                    //meshes[meshID].vertices[i] += ((impactVelocity / 100));
                    //Debug.Log(((trasnform.InverseTransformDirection(impactVelocity)))/10);
                    //meshes[meshID].vertices[i] += ((trasnform.InverseTransformDirection(impactVelocity)) / ((maxSpeed - Mathf.Abs(currentSpeed)) + 50));
                    meshes[meshID].vertices[i] += normalizedImpactVelocity;
                    //Debug.Log((Mathf.RoundToInt(normalizedImpactVelocity.magnitude)));
                    Game.Instance.updateCurrentScore(1);
                    //meshes[meshID].vertices[i] += (impactVelocity / 50);
                    //float kof = (currentSpeed / maxSpeed) + 0.001F;
                    //meshes[meshID].vertices[i] += (trasnform.InverseTransformDirection(impactVelocity)) * kof/100;
                }
            }
        }
    }

}
