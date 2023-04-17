using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VariablesPlayer variablesPlayer;

    private void Start()
    {
        variablesPlayer.inicializarVariables();
    }

    private void LateUpdate()
    {
        variablesPlayer.actualizarUI();
    }
}
