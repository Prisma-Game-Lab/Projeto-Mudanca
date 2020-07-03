using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum tipoTooltip
{
    player,
    adversario,
    alinhamento,
    acao,
    argumento,
    nenhum
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
    private CombateArgumento argumentoAssociado;

    private bool exibindo = false;
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
            case tipoTooltip.acao:
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
            case tipoTooltip.argumento:
                if(argumentoAssociado != null)
                {
                    switch(argumentoAssociado.habilidade)
                    {
                        case CombateArgumento.tipoArgumento.Ataque:
                            textoTooltip = string.Format("Aumentando dano causado.",argumentoAssociado.valor);
                            break;
                        case CombateArgumento.tipoArgumento.Defesa:
                            textoTooltip = string.Format("Reduz dano recebido.",argumentoAssociado.valor);
                            break;
                        case CombateArgumento.tipoArgumento.Evasao:
                            textoTooltip = string.Format("Ignora dano recebido.",argumentoAssociado.valor);
                            break;
                        case CombateArgumento.tipoArgumento.RoubaVida:
                            textoTooltip = string.Format("Recupera parte do dano causado como vida.",argumentoAssociado.valor);
                            break;
                        default:
                            textoTooltip = string.Format("Recuperando vida a cada turno.",argumentoAssociado.valor);
                            break;
                    }
                }
                else textoTooltip = "";
                break;
            default:
                textoTooltip = "";
                break;
        }
        if(exibindo)
        {
            TooltipScript.ExibirTooltip(textoTooltip);
        }
    }
    void OnMouseOver()
    {
        exibindo = true;
        TooltipScript.ExibirTooltip(textoTooltip);
    }
    void OnMouseExit()
    {
        exibindo = false;
        TooltipScript.EsconderTooltip();
    }
    public void OnPointerEnter()
    {
        exibindo = true;
        TooltipScript.ExibirTooltip(textoTooltip);
    }
    public void OnPointerExit()
    {
        exibindo = false;
        TooltipScript.EsconderTooltip();
    }
    public void associaAcao(CombateAcao novaAssociada)
    {
        acaoAssociada = novaAssociada;
    }

    public void associaArgumento(CombateArgumento novoArgumentoAssociado)
    {
        argumentoAssociado = novoArgumentoAssociado;
    }
}
