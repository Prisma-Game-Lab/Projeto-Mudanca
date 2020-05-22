using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipObserver : MonoBehaviour
{
    private string textoTooltip = "";

    //A: uma acao estara associada ao objeto caso ele seja um botao
    private CombateAcao acaoAssociada;
    void Update()
    {
        if (this.GetComponent<CombateAtributos>() != null && this.GetComponent<CombateAtributos>().atributos.nome != "Você")    
        {
            
            textoTooltip = this.GetComponent<CombateAtributos>().atributos.descricao;
        }else if (this.GetComponent<CombateAtributos>() != null && this.GetComponent<CombateAtributos>().atributos.nome == "Você")    
        {
            CombateUnidade atribJogador = this.GetComponent<CombateAtributos>().atributos;
            textoTooltip = string.Format("Ataque: {0}\nDefesa: {1}",atribJogador.dano,atribJogador.defesa);
        }
        else
        {
            switch(acaoAssociada.tipo)
            {
                case CombateAcao.tipoDano.Agressivo:
                    textoTooltip = string.Format("Tipo: Incisivo\nDano: {0}%\nArgumentação: {1}",acaoAssociada.dano,acaoAssociada.barraArgumento);
                    break;
                case CombateAcao.tipoDano.Diplomatico:
                    textoTooltip = string.Format("Tipo: Diplomatico\nDano: {0}%\nArgumentação: {1}",acaoAssociada.dano,acaoAssociada.barraArgumento);
                    break;
                case CombateAcao.tipoDano.Manipulador:
                    textoTooltip = string.Format("Tipo: Defensivo\nDano: {0}%\nArgumentação: {1}",acaoAssociada.dano,acaoAssociada.barraArgumento);
                    break;
                default:
                    textoTooltip = string.Format("Tipo: Neutro\nDano: {0}%\nArgumentação: {1}",acaoAssociada.dano,acaoAssociada.barraArgumento);
                    break;
            }
        }
    }
    void OnMouseOver()
    {
        TooltipScript.ExibirTooltip(textoTooltip);
    }
    void OnMouseExit()
    {
        TooltipScript.EsconderTooltip();
    }
    public void OnPointerEnter()
    {
        TooltipScript.ExibirTooltip(textoTooltip);
    }
    public void OnPointerExit()
    {
        TooltipScript.EsconderTooltip();
    }
    public void associaAcao(CombateAcao novaAssociada)
    {
        acaoAssociada = novaAssociada;
    }
}
