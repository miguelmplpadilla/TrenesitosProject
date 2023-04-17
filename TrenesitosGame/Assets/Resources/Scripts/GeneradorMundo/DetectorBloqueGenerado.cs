using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBloqueGenerado : MonoBehaviour
{
    
    private GameObject locomotora;
    [SerializeField] private VariablesPlayer variablesPlayer;

    [SerializeField] private int vida = 3;
    [SerializeField] private string tipoObjeto = "";

    public bool golpeado = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Start()
    {
        locomotora = GameObject.Find("Locomotora");
    }
    
    void Update()
    {
        float distancia = Vector3.Distance(locomotora.transform.position, transform.position);

        if (transform.position.x < locomotora.transform.position.x && distancia > 60)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void hurt()
    {
        if (variablesPlayer.compararObjetoInventario(tipoObjeto))
        {
            if (!golpeado)
            {
                variablesPlayer.setTipoObjetoInventario(tipoObjeto);
            
                vida--;
                
                animator.SetTrigger("golpear");
                
                variablesPlayer.sumarObjetosInventario(1);

                if (vida <= 0)
                {
                    Destroy(transform.parent.gameObject);
                }

                golpeado = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Via"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
