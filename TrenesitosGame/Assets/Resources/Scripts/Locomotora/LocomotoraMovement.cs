using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocomotoraMovement : MonoBehaviour
{

    private GeneradorVias generadorVias;
    
    public float speed = 2;
    public float currentSpeed = 0;

    [SerializeField] private GameObject viaCercana;
    private GameObject viaCercanaReal;

    private Slider sliderAcelerador;
    
    void Start()
    {
        generadorVias = GameObject.Find("GeneradorVias").GetComponent<GeneradorVias>();
        viaCercana = generadorVias.ultimaVia;
        viaCercanaReal = generadorVias.penultimaVia;
        sliderAcelerador = GameObject.Find("SliderAcelerador").GetComponent<Slider>();
    }

    void Update()
    {
        currentSpeed = speed * sliderAcelerador.value;
        
        Vector3 moveTowardsPosicion =
            Vector3.MoveTowards(transform.position, generadorVias.ultimaVia.transform.position, currentSpeed * Time.deltaTime);

        transform.position = new Vector3(moveTowardsPosicion.x, moveTowardsPosicion.y, transform.position.z);
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
