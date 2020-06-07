using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Nova Lista de Ações", menuName = "Lista de Ações")]
public class CombateAcoesDaFase : ScriptableObject
{
    [Tooltip("Arsenal de ações disponiveis para a unidade. Para o player, deve ter tamanho 3. Qualquer tamanho serve para adversario")]
    public CombateAcao[] ListaAcoes;

    public CombateAcao GetAcao(int index)
    {
        return ListaAcoes[index];
    }

    public bool isEqual(CombateAcoesDaFase comparado)
    {
        if (this.ListaAcoes.Length != comparado.ListaAcoes.Length)
        {
            return false;
        }
        int i;
        for(i=0;i<this.ListaAcoes.Length;i++)
        {
            if(!this.ListaAcoes[i].isEqual(comparado.ListaAcoes[i]))
            {
                return false;
            }
        }
        return true;
    }
}
