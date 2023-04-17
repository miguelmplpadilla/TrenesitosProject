using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "VariablesJugador", menuName = "VariablesJugador")]
public class VariablesPlayer : ScriptableObject
{
    public int cantObjetosInventario = 0;
    public float vida = 10;
    public string tipoObjetoInventario = "";

    private TextMeshProUGUI textoCantidadObjetos;

    public void inicializarVariables()
    {
        cantObjetosInventario = 0;
        vida = 10;
        tipoObjetoInventario = "";

        textoCantidadObjetos = GameObject.Find("TextoCantidadInventario").GetComponent<TextMeshProUGUI>();
    }

    public void restarVida(float num)
    {
        vida -= num;
    }
    
    public void sumarVida(float num)
    {
        vida += num;
    }
    
    public void restarObjetosInventario(int num)
    {
        cantObjetosInventario -= num;
    }
    
    public void sumarObjetosInventario(int num)
    {
        cantObjetosInventario += num;
    }

    public void actualizarUI()
    {
        textoCantidadObjetos.text = cantObjetosInventario.ToString();
    }

    public bool compararObjetoInventario(string tipoComparar)
    {
        return tipoObjetoInventario.Equals(tipoComparar) || tipoObjetoInventario.Equals("");
    }

    public void setTipoObjetoInventario(string tipoInventario)
    {
        tipoObjetoInventario = tipoInventario;
    }
}
