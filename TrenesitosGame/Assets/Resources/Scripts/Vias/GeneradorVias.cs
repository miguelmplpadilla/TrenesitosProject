using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorVias : MonoBehaviour
{
    public GameObject ultimaVia;
    public GameObject penultimaVia;
    [SerializeField] private GameObject via;

    public List<GameObject> vias;

    [SerializeField] private int numViasGenerar;
    [SerializeField] private float distanciaGeneracionVias = 4;

    private GameObject locomotora;

    private int numViasGeneradas = 0;

    private WorldGenerator worldGenerator;

    private void Start()
    {
        locomotora = GameObject.Find("Locomotora");
        worldGenerator = GameObject.Find("GeneradorMapa").GetComponent<WorldGenerator>();
        
        worldGenerator.generarChunk();
        generarVias();
    }

    public void generarVias()
    {
        for (int i = 0; i < numViasGenerar; i++)
        {
            numViasGeneradas++;
            Vector3 posicionVia = new Vector3(ultimaVia.transform.position.x + distanciaGeneracionVias, ultimaVia.transform.position.y,
                ultimaVia.transform.position.z);
            GameObject viaGenerada = Instantiate(via, posicionVia, Quaternion.identity);
            
            viaGenerada.name = "SegmentoVia" + numViasGeneradas;
            
            vias.Add(viaGenerada);
            ultimaVia = viaGenerada;
        }
    }

    private void Update()
    {
        float distLocomotoraUltimaVia = Vector3.Distance(locomotora.transform.position, ultimaVia.transform.position);

        Debug.Log(distLocomotoraUltimaVia);

        if (distLocomotoraUltimaVia < 21)
        {
            generarVias();
            worldGenerator.generarChunk();
        }
    }
}
