using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoMapaController : MonoBehaviour
{
    private DetectorBloqueGenerado detectorBloqueGenerado;

    private void Awake()
    {
        detectorBloqueGenerado = GetComponentInChildren<DetectorBloqueGenerado>();
    }

    public void setGolpeadoFalse()
    {
        detectorBloqueGenerado.golpeado = false;
    }
}
