using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateAtributos : MonoBehaviour
{
    //A: Recebe atributos de um elemento CombateUnidade
    [Tooltip("Referência de um objeto CombateUnidade contendo os atributos desejados para o início do combate")]
    public CombateUnidade atributos;
    [Tooltip("Referências de objetos CombateAcao que descrevem as acoes possiveis no combate")]
    public CombateAcao[] acoes;
    private int vidaAtual;
    void Start()
    {
        vidaAtual = atributos.vida;
    }

    public int getVidaAtual()
    {
        return vidaAtual;
    }
    public CombateUnidade getAtributos() 
    {
        return atributos;
    }

    public void danifica(int danoRecebido)
    {
        vidaAtual = Mathf.Clamp(vidaAtual-danoRecebido,0,atributos.vida);
        Debug.Log("Vida atualizada para:");
        Debug.Log(vidaAtual);
    }
}
