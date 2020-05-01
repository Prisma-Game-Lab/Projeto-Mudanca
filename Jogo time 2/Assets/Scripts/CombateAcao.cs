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
    [Tooltip("Nome que aparecerá no botão")]
    //nome: o que sera mostrado no botao
    public string nome;

    [Tooltip("Multiplicador do atributo de dano de quem executa a ação, em %")]
    //dano: porcentagem, multiplicador sobre o atributo de ataque da unidade
    public float dano;

    [Tooltip("Tipo do dano causado (Agressivo, Manipulador, Diplomatico ou Neutro). Ordem (Derrota > Derrotado): Agressivo > Manipulador > Diplomático > Agressivo")]
    //tipo: para definir se alvo tem resistencias ou desvantagens
    public tipoDano tipo;
}
