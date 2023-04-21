using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjetoMapaController : MonoBehaviour
{
    private DetectorBloqueGenerado detectorBloqueGenerado;
    private MeshRenderer meshRenderer;

    [SerializeField] private LayerMask layerMask;
    
    private bool movido = false;

    private void Awake()
    {
        detectorBloqueGenerado = GetComponentInChildren<DetectorBloqueGenerado>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void LateUpdate()
    {
        if (!movido)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                transform.position = hit.point;
                meshRenderer.shadowCastingMode = ShadowCastingMode.On;
                movido = true;
            }
        }
    }

    public void setGolpeadoFalse()
    {
        detectorBloqueGenerado.golpeado = false;
    }
}
