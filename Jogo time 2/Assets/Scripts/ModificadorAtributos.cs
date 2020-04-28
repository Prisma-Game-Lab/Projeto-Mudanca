using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadorAtributos : MonoBehaviour
{
     public CombateUnidade NovoAtributo;
 
     private CombateAtributos AtributoAtual;
     
   
     public int Vida;
    int VidaAtual;
    // Start is called before the first frame update
    void Start()
    {
       
        AtributoAtual= gameObject.GetComponent<CombateAtributos>();
         
    }

    // Update is called once per frame
    void Update()
    {
        VidaAtual= AtributoAtual.getVidaAtual();
        if (VidaAtual<=Vida){
            ModificaAtributo(NovoAtributo);
        }
    }

    void ModificaAtributo(CombateUnidade NovoAtributo){
            AtributoAtual.atributos=NovoAtributo;
            this.enabled=false;
            

    }
}
