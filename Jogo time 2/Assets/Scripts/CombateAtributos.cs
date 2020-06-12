using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateAtributos : MonoBehaviour
{
    //A: Recebe atributos de um elemento CombateUnidade
    [Tooltip("Referência de um objeto CombateUnidade contendo os atributos desejados para o início do combate")]
    public CombateUnidade atributos;

    /*[Tooltip("Atributos em cada fase da batalha")]
    public CombateUnidade[] atributosDaFase;*/

    [Tooltip("Listas de ações de cada fase da batalha. Tamanho deve ser igual ao numero de fases")]
    public CombateAcoesDaFase[] listaAcoesDaFase;
    
    [Tooltip("Listas dos argumentos de cada fase da batalha. Tamanho deve ser igual ao numero de fases")]
    public CombateArgsDaFase[] listaArgsDaFase;
    private CombateAcoesDaFase listaAcoesAtual;
    private int fase;
    private CombateAcao[] acoes;
    private int vidaAtual;
    private int argumentoAtual;
    private float auxiliar;
    private CombateAcao tempGO;
    private CombateArgumento argAgre, argDipl, argDef;

    void Awake()
    {
        listaAcoesAtual = listaAcoesDaFase[0];
        setAcoes(listaAcoesAtual);
        setArgs(listaArgsDaFase[0]);
    }
    void Start()
    {
        vidaAtual = atributos.vida;
        argumentoAtual = 0;
    }

    void Update()
    {
        if (!listaAcoesAtual.isEqual(listaAcoesDaFase[fase]))
        {
            listaAcoesAtual = listaAcoesDaFase[fase];
            this.setAcoes(listaAcoesDaFase[fase]);
            this.setArgs(listaArgsDaFase[fase]);
        }
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
        for(int i = 0; i < acoes.Length; i++)
        {
            
            acoes[i].ShuffleFalas();
            
        }
        
    }
    public void setAcoes(CombateAcoesDaFase novasAcoes)
    {
        this.acoes = novasAcoes.ListaAcoes;
    }
    public void setArgs(CombateArgsDaFase novosArgs)
    {
        this.argAgre = novosArgs.GetAgre();
        this.argDipl = novosArgs.GetDipl();
        this.argDef = novosArgs.GetDef();
    }
    public CombateArgumento getArgAgre()
    {
        return argAgre;
    }
    public CombateArgumento getArgDipl()
    {
        return argDipl;
    }
    public CombateArgumento getArgDef()
    {
        return argDef;
    }
    public CombateAcao getAcao(int index)
    {
        return acoes[index];
    }
    public int getLengthAcoes()
    {
        return acoes.Length;
    }

    public void setFase(int novaFase)
    {
        fase = novaFase;
    }

    public void curar(int quantidade)
    {
        vidaAtual += quantidade;
    }

}
