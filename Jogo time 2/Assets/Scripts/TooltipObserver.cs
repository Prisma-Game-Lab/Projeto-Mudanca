using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum tipoTooltip
{
    player,
    adversario,
    alinhamento,
    acao
}
public class TooltipObserver : MonoBehaviour
{
    //A: identifica como sera buscado o texto a ser adicionado na tooltip
    public tipoTooltip classificacao;
    private string textoTooltip = "";

    //A: apenas importante se tipoTooltip for alinhamento
    public Slider alinhamentoSlider;

    //A: uma acao estara associada ao objeto caso ele seja um botao
    private CombateAcao acaoAssociada;
    void Update()
    {
        switch(classificacao)
        {
            case tipoTooltip.adversario:
                textoTooltip = this.GetComponent<CombateAtributos>().atributos.descricao;
                break;
            case tipoTooltip.player:
                CombateUnidade atribJogador = this.GetComponent<CombateAtributos>().atributos;
                textoTooltip = string.Format("Ataque: {0}\nDefesa: {1}",atribJogador.dano,atribJogador.defesa);
                break;
            case tipoTooltip.alinhamento:
                switch(alinhamentoSlider.value)
                {
                    case 0:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x3\nDiplomatico: x2\nDefensivo: x2";
                        break;
                    case 1:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x2.5\nDiplomatico: x2.5\nDefensivo: x2";
                        break;
                    case 2:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x2\nDiplomatico: x3\nDefensivo: x2";
                        break;
                    case 3:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x2\nDiplomatico: x2.5\nDefensivo: x2.5";
                        break;
                    case 4:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x2\nDiplomatico: x2\nDefensivo: x3";
                        break;
                    default:
                        textoTooltip = "Bonus de dano efetivo:\nIncisivo: x2\nDiplomatico: x2\nDefensivo: x2";
                        break;


                }
                break;
            default:
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
                break;


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
