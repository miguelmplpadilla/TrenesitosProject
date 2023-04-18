using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{

    public Vector3Int chunkSize = new Vector3Int(16, 256, 16);
    public Vector2 noiseScale = Vector2.one;
    public Vector2 noiseOffset = Vector2.zero;
    [Space] 
    public int heightOffset = 60;
    public float heightIntensity = 5;
    private int[,,] tempData;

    [SerializeField] private GameObject prefabArbol;
    [SerializeField] private GameObject prefabRoca;

    [SerializeField] private int numVecesGenerar = 2;

    private bool generado;
    private int numChunksGenerados = 0;

    [SerializeField] private GameObject inicioGeneracionMapa;
    
    
    void Start()
    {
        //generarChunk();
    }

    public void generarChunk()
    {
        tempData = new int[chunkSize.x, chunkSize.y, chunkSize.z];
        
        for (int i = 0; i < numVecesGenerar; i++)
        {
            generarMapeado(Random.Range(1,2+1));
        }
        
        generarMundo();
    }

    private void generarMapeado(int tipoBloqueGenerar)
    { 
        noiseOffset = new Vector2(Random.Range(0.0f, 10f), Random.Range(0.0f, 10f));
        //noiseScale = new Vector2(Random.Range(0.0f, 10f), Random.Range(0.0f, 10f));

        for (int i = 0; i < chunkSize.x; i++)
        {
            for (int j = 0; j < chunkSize.z; j++)
            {
                float perlinCordX = noiseOffset.x + i / (float)chunkSize.x * noiseScale.x;
                float perlinCordY = noiseOffset.y + j / (float)chunkSize.z * noiseScale.y;
                int heightGen =
                    Mathf.RoundToInt(Mathf.PerlinNoise(perlinCordX, perlinCordY) * heightIntensity + heightOffset);

                for (int k = heightGen; k >= 0; k--)
                {
                    if (tempData[i, 0, j] == 0 || tempData[i, 0, j] == 2)
                    {
                        if (k == heightGen)
                        {
                            tempData[i, 0, j] = tipoBloqueGenerar;
                        }

                        if (k == 2)
                        {
                            tempData[i, 0, j] = 0;
                        }
                    }
                }
            }
        }
    }

    private void generarMundo()
    {
        if (tempData != null)
        {
            GameObject chunk = new GameObject();
            for (int i = 0; i < chunkSize.x; i++)
            {
                for (int j = 0; j < chunkSize.y; j++)
                {
                    for (int k = 0; k < chunkSize.z; k++)
                    {
                        GameObject prefabAInstanciar = null;
                        
                        switch (tempData[i,j,k])
                        {
                            case 1:
                                generado = true;
                                prefabAInstanciar = prefabArbol;
                                break;
                            
                            case 2:
                                generado = true;
                                prefabAInstanciar = prefabRoca;
                                break;
                        }

                        if (prefabAInstanciar != null)
                        {
                            GameObject prefabInstanciado = Instantiate(prefabAInstanciar, new Vector3(inicioGeneracionMapa.transform.position.x + i + (chunkSize.x * numChunksGenerados), 0, inicioGeneracionMapa.transform.position.z + k), Quaternion.identity);
                            prefabInstanciado.transform.parent = chunk.transform;
                        }
                    }
                }
            }

            if (!generado)
            {
                Destroy(chunk);
                generarChunk();
            }
            else
            {
                numChunksGenerados++;
                chunk.transform.localScale = new Vector3(3, 3, 3);
                chunk.name = "Chunk" + numChunksGenerados;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
