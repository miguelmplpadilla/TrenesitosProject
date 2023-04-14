using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotoraMovement : MonoBehaviour
{

    private GeneradorVias generadorVias;
    public float speed = 2;

    [SerializeField] private GameObject viaCercana;
    private GameObject viaCercanaReal;
    
    void Start()
    {
        generadorVias = GameObject.Find("GeneradorVias").GetComponent<GeneradorVias>();
        viaCercana = generadorVias.ultimaVia;
        viaCercanaReal = generadorVias.penultimaVia;
    }

    void Update()
    {
        //lookAtlocomotora();

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Vector3 moveTowardsPosicion =
                Vector3.MoveTowards(transform.position, generadorVias.ultimaVia.transform.position, speed * Time.deltaTime);

            transform.position = new Vector3(moveTowardsPosicion.x, moveTowardsPosicion.y, transform.position.z);
        }
    }

    private void lookAtlocomotora()
    {
        detectarViaCercana();
        transform.LookAt(viaCercana.transform);
    }

    private void detectarViaCercana()
    {
        for (int i = 0; i < generadorVias.vias.Count; i++)
        {
            float distancia = Vector3.Distance(transform.position, generadorVias.vias[i].transform.position);

            if (distancia < Vector3.Distance(transform.position, viaCercanaReal.transform.position))
            {
                viaCercanaReal = generadorVias.vias[i];
                viaCercana = generadorVias.vias[i+2];
            }
        }
    }
}
