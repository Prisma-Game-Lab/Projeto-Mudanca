using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Novo Argumento", menuName = "Argumento")]
public class CombateArgumento : ScriptableObject
{
    public enum tipoArgumento
    {
        Regenerar,
        Defesa,
        Ataque,
        Evasao,
        RoubaVida
    }

    [Header("Atributos de uma argumento em combate:")]
    //A: Atributos de um argumento:
    [Tooltip("Nome que surge na tooltip")]
    //nome: nome do argumento apresentado na UI
    public string nome;

    [Tooltip("Qual tipo de efeito o argumento causa")]
    public tipoArgumento habilidade;

    [Tooltip("Valor do efeito do argumento")]
    public float valor;

    [Tooltip("Duração máxima do argumento, em turnos")]
    public int duracao;
    
    [Tooltip("Dano necessario para destruir argumento")]
    public int vida;

}