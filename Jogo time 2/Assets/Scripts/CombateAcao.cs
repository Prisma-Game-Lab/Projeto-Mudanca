using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Nova Ação", menuName = "Ação")]
public class CombateAcao : ScriptableObject
{
    public enum tipoDano
    {
        Agressivo,
        Manipulador,
        Diplomatico,
        Neutro
    }

    //A: atributos de um ataque:
    [Header("Dados de uma ação:")]
    [Tooltip("Nomes que aparecerão no botão. Escolhidos aleatoriamente a cada turno")]
    //nome: o que sera mostrado no botao
    public string[] nome;

    [Tooltip("Respostas para as falas descritas em 'nome'. Deve ser do mesmo tamanho. A resposta é para a fala da mesma linha")]
    public string[] respostas;

    [Tooltip("Multiplicador do atributo de dano de quem executa a ação, em %")]
    //dano: porcentagem, multiplicador sobre o atributo de ataque da unidade
    public float dano;

    [Tooltip("Quantidade carregada da barra de argumentos ao utilizar o ataque")]
    public int barraArgumento;

    [Tooltip("Tipo do dano causado.")]
    //tipo: para definir se alvo tem resistencias ou desvantagens
    public tipoDano tipo;

    
    public bool isEqual(CombateAcao comparado)
    {
        if (this.nome == comparado.nome && this.dano == comparado.dano && this.barraArgumento == comparado.barraArgumento && this.tipo == comparado.tipo)
        {
            return true;
        }
        else return false;
    }

    public void ShuffleFalas()
    {
        string falaTemp,respostaTemp;
        
        
        int rnd = Random.Range(0, nome.Length);
        falaTemp = nome[rnd];
        respostaTemp = respostas[rnd];
        nome[rnd] = nome[0];
        respostas[rnd] = respostas[0];
        nome[0] = falaTemp;
        respostas[0] = respostaTemp;
        
    }
}
