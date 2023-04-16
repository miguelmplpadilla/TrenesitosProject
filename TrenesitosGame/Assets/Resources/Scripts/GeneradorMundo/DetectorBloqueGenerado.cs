using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBloqueGenerado : MonoBehaviour
{
    
    private GameObject locomotora;
    
    void Start()
    {
        locomotora = GameObject.Find("Locomotora");
    }
    
    void Update()
    {
        float distancia = Vector3.Distance(locomotora.transform.position, transform.position);

        if (transform.position.x < locomotora.transform.position.x && distancia > 50)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Via"))
        {
            Destroy(transform.parent.gameObject);
        }

        if (other.CompareTag("HitBoxPlayer"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            Destroy(transform.parent.gameObject);
        }
    }
}
