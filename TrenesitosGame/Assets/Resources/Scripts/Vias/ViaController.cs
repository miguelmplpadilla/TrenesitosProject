using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ViaController : MonoBehaviour
{
    private GameObject locomotora;
    private GeneradorVias generadorVias;
    
    void Start()
    {
        generadorVias = GameObject.Find("GeneradorVias").GetComponent<GeneradorVias>();
        locomotora = GameObject.Find("Locomotora");
    }

    
    void Update()
    {
        float distancia = Vector3.Distance(locomotora.transform.position, transform.position);

        if (transform.position.x < locomotora.transform.position.x && distancia > 50)
        {
            generadorVias.vias.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
