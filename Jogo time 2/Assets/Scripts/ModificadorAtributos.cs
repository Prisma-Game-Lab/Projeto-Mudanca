﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadorAtributos : MonoBehaviour
{
    public Modificador[] NovosAtributos;

    private CombateAtributos AtributoAtual;

    private int i;

    private int VidaAtual;
    // Start is called before the first frame update
    void Start()
    {

        AtributoAtual = gameObject.GetComponent<CombateAtributos>();
        i = 0;

    }

    // Update is called once per frame
    void Update()
    {
        VidaAtual = AtributoAtual.getVidaAtual();

        if (VidaAtual <= NovosAtributos[i].Vida)
        {
             
                ModificaAtributo(NovosAtributos[i].Atributo);
                NovosAtributos[i].Atributo.vida = VidaAtual;
                i++;
                } 
    }
        void ModificaAtributo(CombateUnidade NovoAtributo)
        {

            AtributoAtual.atributos = NovoAtributo;




        }
    }