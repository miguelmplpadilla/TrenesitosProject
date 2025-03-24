using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjetoMapaController : MonoBehaviour
{
    private DetectorBloqueGenerado detectorBloqueGenerado;
    private MeshRenderer meshRenderer;
    private Animator animator;

    [SerializeField] private LayerMask layerMask;
    
    private bool movido = false;

    private void Awake()
    {
        detectorBloqueGenerado = GetComponentInChildren<DetectorBloqueGenerado>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (!movido)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                transform.position = hit.point;
                meshRenderer.enabled = true;
                meshRenderer.shadowCastingMode = ShadowCastingMode.On;
                movido = true;
            }
        }
    }
    
    public void interEnter()
    {
        if (detectorBloqueGenerado.variablesPlayer.compararObjetoInventario(detectorBloqueGenerado.tipoObjeto))
        {
            animator.SetBool("seleccionado", true);
        }
    }

    public void interExit()
    {
        animator.SetBool("seleccionado", false);
    }

    public void setGolpeadoFalse()
    {
        detectorBloqueGenerado.golpeado = false;
    }
}
