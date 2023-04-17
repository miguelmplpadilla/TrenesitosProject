using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeclaInteractuarController : MonoBehaviour
{
    private GameObject modelo;
    private GameObject CM;

    private void Awake()
    {
        modelo = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        CM = GameObject.Find("CM");
    }

    private void Update()
    {
        transform.LookAt(CM.transform);
    }

    public void mostrarTecla()
    {
        modelo.SetActive(true);
    }

    public void esconderTecla()
    {
        modelo.SetActive(false);
    }
}
