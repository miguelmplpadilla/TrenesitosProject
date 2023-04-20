using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MeshGenerator : MonoBehaviour
{

    private Mesh mesh;

    [SerializeField] private Vector3[] vertices;
    [SerializeField] private int[] triangles;

    public int numVerticesPlanos = 2;
    public float offsetPerlinNoise = 0;

    private int offsetPosicionGeneracion = 0;

    public GameObject prefabMesh;

    private int xPerinNoise = 0;
    private int yPerinNoise = 0;

    private int xSizeAnterior = 0;
    private int zSizeAnterior = 0;
    
    public Vector2Int chunkSize = new Vector2Int(20, 20);
    public Vector2 noiseScale = Vector2.one;
    public Vector2 noiseOffset = Vector2.zero;
    [Space] 
    public int heightOffset = 60;
    public float heightIntensity = 5;

    public int direccion = 1;

    void Start()
    {
        xSizeAnterior = chunkSize.x;
        zSizeAnterior = chunkSize.y;
        
        noiseScale = new Vector2(Random.Range(3.0f, 10f), Random.Range(3.0f, 10f));
        
        createShape();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            createShape();
        }
        
        //updateShape();
    }

    private void createShape()
    {
        GameObject objetoMesh = Instantiate(prefabMesh, new Vector3(0 + offsetPosicionGeneracion, 0, 0), Quaternion.identity);
        
        objetoMesh.transform.localScale = new Vector3(1, 1, direccion);
        
        mesh = new Mesh();
        objetoMesh.GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(chunkSize.x + 1) * (chunkSize.y + 1)];
        
        int cont = 0;
        
        for (int z = 0; z < numVerticesPlanos; z++)
        {
            for (int x = 0; x <= chunkSize.x; x++)
            {
                vertices[cont] = new Vector3(x, 0, z);
                cont++;
            }
        }
        
        for (int i = cont, z = numVerticesPlanos; z <= chunkSize.y; z++)
        {
            for (int x = 0; x <= chunkSize.x; x++)
            {
                float perlinCordX = noiseOffset.x + (x + offsetPerlinNoise) / (float)chunkSize.x * noiseScale.x;
                float perlinCordY = noiseOffset.y + z / (float)chunkSize.y * noiseScale.y;

                float y = Mathf.PerlinNoise(perlinCordX, perlinCordY) * 2;
                
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        
        triangles = new int[chunkSize.x * chunkSize.y * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < chunkSize.y; z++)
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + chunkSize.x + 1;
                triangles[tris + 2] = vert + 1;
        
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + chunkSize.x + 1;
                triangles[tris + 5] = vert + chunkSize.x + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }

        offsetPosicionGeneracion += 20;
        offsetPerlinNoise += 20;
        
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
        if (chunkSize.x != xSizeAnterior || chunkSize.y != zSizeAnterior)
        {
            vertices = new Vector3[(chunkSize.x + 1) * (chunkSize.y + 1)];
        
            int cont = 0;
        
            for (int z = 0; z < numVerticesPlanos; z++)
            {
                for (int x = 0; x <= chunkSize.x; x++)
                {
                    vertices[cont] = new Vector3(x, 0, z);
                    cont++;
                }
            }
            
            noiseScale = new Vector2(Random.Range(0.0f, 10f), Random.Range(0.0f, 10f));
        
            for (int i = cont, z = numVerticesPlanos; z <= chunkSize.y; z++)
            {
                for (int x = 0; x <= chunkSize.x; x++)
                {
                    float perlinCordX = noiseOffset.x + x / (float)chunkSize.x * noiseScale.x;
                    float perlinCordY = noiseOffset.y + z / (float)chunkSize.y * noiseScale.y;

                    float y = Mathf.PerlinNoise(perlinCordX, perlinCordY) * 2;
                    
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }
        
            triangles = new int[chunkSize.x * chunkSize.y * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z < chunkSize.y; z++)
            {
                for (int x = 0; x < chunkSize.x; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + chunkSize.x + 1;
                    triangles[tris + 2] = vert + 1;
        
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + chunkSize.x + 1;
                    triangles[tris + 5] = vert + chunkSize.x + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            xSizeAnterior = chunkSize.x;
            zSizeAnterior = chunkSize.y;
            
            updateMesh();
            
            //objetoMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
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
