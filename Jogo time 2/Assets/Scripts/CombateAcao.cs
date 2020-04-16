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
    //nome: o que sera mostrado no botao
    public string nome;

    //dano: porcentagem, multiplicador sobre o atributo de ataque da unidade
    public float dano;

    //tipo: para definir se alvo tem resistencias ou desvantagens
    public tipoDano tipo;
}
