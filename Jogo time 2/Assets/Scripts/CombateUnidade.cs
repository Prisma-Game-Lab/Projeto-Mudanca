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

    [Header("Atributos de uma unidade em combate:")]
    //A: Atributos de uma unidade:
    [Tooltip("Nome que surge na barra de vida")]
    //nome: nome da unidade apresentado na UI
    public string nome;
    
    [Tooltip("Vida máxima e inicial da unidade")]
    //vida: vitalidade maxima. determina hp inicial no combate
    public int vida;

    [Tooltip("Dano base dos ataques, multiplicado pela ação escolhida")]
    //dano: dano base de ataque. multiplicado dependendo do ataque escolhido
    public int dano;
    
    [Tooltip("Subtrai o dano recebido. Recebe um mínimo de 1 de dano")]
    //defesa: reducao de dano recebido
    public int defesa;
    
    [Tooltip("Determina quem atacará primeiro")]
    //iniciativa: determina ordem do combate (talvez seja descartado)
    public int iniciativa;
 
    [Tooltip("Tipo da unidade (Agressivo, Manipulador, Diplomatico ou Neutro), para calculo de efetividade das ações. Ordem (Derrota > Derrotado): Agressivo > Manipulador > Diplomático > Agressivo. ")]
    //tipo: determina resistencias e desvantagens. Neutro leva dano base de qualquer tipo
    public tipoUnidade tipo;
}
