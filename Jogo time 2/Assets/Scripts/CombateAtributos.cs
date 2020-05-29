using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateAtributos : MonoBehaviour
{
    //A: Recebe atributos de um elemento CombateUnidade
    [Tooltip("Referência de um objeto CombateUnidade contendo os atributos desejados para o início do combate")]
    public CombateUnidade atributos;
    [Tooltip("Referências de objetos CombateAcao que descrevem as acoes possiveis no combate. Ultima ação é reservada para a postura ATAQUE ESMAGADOR")]
    public CombateAcao[] acoes;
    private int vidaAtual;
    private int argumentoAtual;
    private float auxiliar;
    private CombateAcao tempGO;
    void Start()
    {
        vidaAtual = atributos.vida;
        argumentoAtual = 0;
    }

    public int getVidaAtual()
    {
        return vidaAtual;
    }

    public int getArgumentos()
    {
        return argumentoAtual;
    }
    public void setArgumentos(int newArgumento)
    {
        argumentoAtual = newArgumento;
    }
    public CombateUnidade getAtributos() 
    {
        return atributos;
    }

    public void danifica(int danoRecebido)
    {
        vidaAtual = Mathf.Clamp(vidaAtual-danoRecebido,0,atributos.vida);
    }

    public bool isResistente(int tipoRecebido)
    {
        for (int i=0;i< atributos.tipoResistente.Length; i++)
        {
            if (tipoRecebido == (int)atributos.tipoResistente[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool isVulneravel(int tipoRecebido)
    {
        for (int i=0;i< atributos.tipoVulneravel.Length; i++)
        {
            if (tipoRecebido == (int)atributos.tipoVulneravel[i])
            {
                return true;
            }
        }
        return false;
    }
    public float getAuxiliar()
    {
        return auxiliar;
    }
    public void setAuxiliar(float novoValor)
    {
        auxiliar = novoValor;
    }

    public void Shuffle() 
    {
        for (int i = 0; i < acoes.Length; i++) 
        {
            int rnd = Random.Range(0, acoes.Length);
            tempGO = acoes[rnd];
            acoes[rnd] = acoes[i];
            acoes[i] = tempGO;
        }
    }
}
