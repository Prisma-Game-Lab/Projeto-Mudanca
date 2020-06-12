using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Nova Lista de Argumentos", menuName = "Lista de Argumentos")]
public class CombateArgsDaFase : ScriptableObject
{
    [Tooltip("Arsenal de argumentos disponiveis para a unidade. Argumento criado depende de qual tipo de ação foi mais usado")]
    public CombateArgumento argAgre, argDipl, argDef;


    public CombateArgumento GetAgre()
    {
        return argAgre;
    }

    public CombateArgumento GetDipl()
    {
        return argDipl;
    }

    public CombateArgumento GetDef()
    {
        return argDef;
    }
}
