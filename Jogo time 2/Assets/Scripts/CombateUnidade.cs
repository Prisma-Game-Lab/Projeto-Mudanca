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

    //A: Atributos de uma unidade:
    //nome: nome da unidade apresentado na UI
    public string nome;
    
    //vida: vitalidade maxima. determina hp inicial no combate
    public int vida;

    //dano: dano base de ataque. multiplicado dependendo do ataque escolhido
    public int dano;

    //defesa: reducao de dano recebido
    public int defesa;

    //iniciativa: determina ordem do combate (talvez seja descartado)
    public int iniciativa;

    //tipo: determina resistencias e desvantagens. Neutro leva dano base de qualquer tipo
    public tipoUnidade tipo;
}
