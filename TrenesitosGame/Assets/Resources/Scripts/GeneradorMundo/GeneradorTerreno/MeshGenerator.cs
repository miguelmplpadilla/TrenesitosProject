using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeshGenerator : MonoBehaviour
{

    private Mesh mesh;

    [SerializeField] private Vector3[] vertices;
    [SerializeField] private int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public int numVerticesPlanos = 2;
    public float offsetPerlinNoise = 0;

    private int offsetPosicionGeneracion = 0;

    public GameObject prefabMesh;

    private int xPerinNoise = 0;
    private int yPerinNoise = 0;

    private GameObject objetoMesh;

    private int xSizeAnterior = 0;
    private int zSizeAnterior = 0;
    
    void Start()
    {
        objetoMesh = Instantiate(prefabMesh, new Vector3(0 + offsetPosicionGeneracion, 0, 0), Quaternion.identity);
        createShape(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            createShape(1);
        }
        
        updateShape();
    }

    private void createShape(int direccion)
    {
        objetoMesh.transform.localScale = new Vector3(1, 1, direccion);
        
        mesh = new Mesh();
        objetoMesh.GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        int cont = 0;
        
        for (int z = 0; z < numVerticesPlanos; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[cont] = new Vector3(x, 0, z);
                cont++;
            }
        }
        
        for (int i = cont, z = numVerticesPlanos; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(xPerinNoise * .3f, yPerinNoise * .3f) * 2;
                Debug.Log(y);
                vertices[i] = new Vector3(x, y, z);
                i++;
                xPerinNoise++;
            }
            
            yPerinNoise++;
        }
        
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
        
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }

        offsetPosicionGeneracion += 20;
        offsetPerlinNoise++;
        
        updateMesh();
        
        objetoMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    private void updateShape()
    {
        if (xSize != xSizeAnterior || zSize != zSizeAnterior)
        {
            vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
            int cont = 0;
        
            for (int z = 0; z < numVerticesPlanos; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    vertices[cont] = new Vector3(x, 0, z);
                    cont++;
                }
            }
        
            for (int i = cont, z = numVerticesPlanos; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2;
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }
        
            triangles = new int[xSize * zSize * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;
        
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            xSizeAnterior = xSize;
            zSizeAnterior = zSize;
            
            updateMesh();
            
            objetoMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }
}
