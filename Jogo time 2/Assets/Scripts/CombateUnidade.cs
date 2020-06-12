using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Nova Unidade", menuName = "Unidade")]
public class CombateUnidade : ScriptableObject
{
    public enum tipoUnidade
    {
        Agressivo,
        Manipulador,
        Diplomatico,
        Neutro
    }
    public enum posturaUnidade
    {
        player,
        reageAgressivo,
        golpeEsmagador,
        ignoraManipulador
    }

    [Header("Atributos de uma unidade em combate:")]
    //A: Atributos de uma unidade:
    [Tooltip("Nome que surge na barra de vida")]
    //nome: nome da unidade apresentado na UI
    public string nome;
    
    [Tooltip("Vida máxima e inicial da unidade")]
    //vida: vitalidade maxima. determina hp inicial no combate
    public int vida;

    [Tooltip("Tamanho da barra de argumento")]
    //barraArgumento: tamanho máximo da barra de argumento. Cria argumento quando cheia
    public int barraArgumento;

    [Tooltip("Dano base dos ataques, multiplicado pela ação escolhida")]
    //dano: dano base de ataque. multiplicado dependendo do ataque escolhido
    public int dano;
    
    [Tooltip("Subtrai o dano recebido. Recebe um mínimo de 1 de dano")]
    //defesa: reducao de dano recebido
    public int defesa;
    
    [Tooltip("Determina quem atacará primeiro")]
    //iniciativa: determina ordem do combate (talvez seja descartado)
    public int iniciativa;
 
    [Tooltip("Tipos aos quais a unidade é resistente")]
    //tipoResistente: determina resistencias da unidade
    public tipoUnidade[] tipoResistente;

    [Tooltip("Tipos aos quais a unidade é vulnerável")]
    //tipoVulneravel: determina vulnerabilidades da unidade
    public tipoUnidade[] tipoVulneravel;

    [Tooltip("Define vantagens passivas em combate")]
    //postura: comportamento da unidade, provendo habilidades passivas ou formas diferentes de tomada de decisão
    public posturaUnidade postura;

    [Tooltip("Descrição da postura, que aparece na tooltip da unidade")]
    //descricao: informacao sobre o efeito da postura. Exibido na tooltip
    public string descricao;
}
