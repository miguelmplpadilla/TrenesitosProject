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
    
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape(-1);
        updateMesh();
        
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void createShape(float posNeg)
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
            offsetPerlinNoise += 0.2f;
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * offsetPerlinNoise, z * offsetPerlinNoise) * 2;
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
    }

    void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
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
