using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombateManager : MonoBehaviour
{
    //A: de quem é o turno
    enum turno{
        jogador,
        adversario
    }

    //A:referencias dos objetos de jogador e adversario na cena
    public GameObject Player, Adversario;

    public Material CorDiplomatico,CorManipulador,CorOfensivo;
    private Color CorAtual;

    //A: referencia de objeto na UI para impedir jogador de apertar botoes na vez do adversario
    public GameObject vezDoOutro;

    //A: referencia as barras de vida e velocidade que descem
    public Slider VidaPlayer, VidaAdversario;
    public float VelocidadeVida;

    //A: Telas Placeholder de vitoria e derrota
    public GameObject TelaVitoria, TelaDerrota;

    //A: tempo aguardado entre cada turno, para o jogador compreender o que ocorre.
    public float entreTurnos;
    
    //A: dono da vez
    private turno turnoAtual;

    //A: armazena atributos para referencias futuras
    private CombateAtributos atributosPlayer, atributosAdversario;

 

    //A: nomes na UI do jogador e do adversario
    public Text nomePlayer, nomeAdversario, vidaPlayerTexto, vidaAdversarioTexto;

    //A: nomes das ações do jogador
    public Text[] nomeAcoes;




    void Start()
    {
        
        atributosPlayer = Player.GetComponent<CombateAtributos>();
        atributosAdversario = Adversario.GetComponent<CombateAtributos>();

        //A: Atualiza nomes de player e adversario na hud
        nomePlayer.text = atributosPlayer.atributos.nome;
        nomeAdversario.text = atributosAdversario.atributos.nome;

        //A: Cada ação do jogador substitui o nome escrito em um dos botoes.
        for(int i=0; i< nomeAcoes.Length;i++)
        {
            nomeAcoes[i].text = atributosPlayer.acoes[i].nome;
        }
        VidaPlayer.maxValue = atributosPlayer.atributos.vida;
        VidaAdversario.maxValue = atributosAdversario.atributos.vida;
        vidaPlayerTexto.text= ""+VidaPlayer;
        vidaAdversarioTexto.text= ""+VidaAdversario;
        //A: Decide de quem sera o primeiro turno
        if (atributosAdversario.getAtributos().iniciativa > atributosPlayer.getAtributos().iniciativa)
        {
            turnoAtual = turno.adversario;
            vezDoOutro.SetActive(true);
            turnoAdversario();
        }
        else 
        {
            turnoAtual = turno.jogador;
            vezDoOutro.SetActive(false);
        }

    }

    void Update()
    {
        vidaPlayerTexto.text= ""+ (int)(atributosPlayer.getVidaAtual());
        vidaAdversarioTexto.text= ""+(int)(atributosAdversario.getVidaAtual());
        VidaPlayer.value = Mathf.Lerp(VidaPlayer.value,atributosPlayer.getVidaAtual(),VelocidadeVida * Time.deltaTime);
        VidaAdversario.value = Mathf.Lerp(VidaAdversario.value,atributosAdversario.getVidaAtual(),VelocidadeVida * Time.deltaTime);
        CorAtual= Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        
    }

    public void Acao1()
    {
        aplicaDano(atributosPlayer,atributosPlayer.acoes[0],atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    public void Acao2()
    {
        aplicaDano(atributosPlayer,atributosPlayer.acoes[1],atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    public void Acao3()
    {
        aplicaDano(atributosPlayer,atributosPlayer.acoes[2],atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

      public void Acao4()
    {
        mudaCor(atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    private void aplicaDano (CombateAtributos atributosAtacante,CombateAcao golpe, CombateAtributos atributosAlvo)
    {
        int ataque = atributosAtacante.atributos.dano;
        float multiplicadorGolpe = golpe.dano/100;
        int danoResultante;

        if(atributosAlvo.atributos.tipo != CombateUnidade.tipoUnidade.Neutro && golpe.tipo != CombateAcao.tipoDano.Neutro)
        {
            //A: ajusta multiplicador conforme tipo
            if( ((int)golpe.tipo+1)%3 == (int)atributosAlvo.atributos.tipo)
            {
                multiplicadorGolpe *= 2;
                Debug.Log("foi super efetivo!");
                Debug.Log(golpe.tipo);
                Debug.Log(atributosAlvo.atributos.tipo);
            }
            else if(((int)atributosAlvo.atributos.tipo+1)%3 == (int)golpe.tipo)
            {
                multiplicadorGolpe = multiplicadorGolpe/2;
                Debug.Log("nao foi efetivo...");
                Debug.Log(golpe.tipo);
                Debug.Log(atributosAlvo.atributos.tipo);
            }
        }
        Debug.Log("Multiplicador: ");
        Debug.Log(multiplicadorGolpe);
        danoResultante = (int) (ataque*multiplicadorGolpe-atributosAlvo.atributos.defesa);
        Debug.Log("Dano calculado. Causando:");
        Debug.Log(danoResultante);
        atributosAlvo.danifica(danoResultante);
    }

    private void turnoAdversario()
    {
        int ataqueEscolhido = Random.Range(0, atributosAdversario.acoes.Length);
        aplicaDano(atributosAdversario,atributosAdversario.acoes[ataqueEscolhido],atributosPlayer);
        StartCoroutine(passaTurno());
    }

    private void endBattle(bool defeat)
    {
        if (defeat)
        {
            TelaDerrota.SetActive(true);
        }
        else{
            TelaVitoria.SetActive(true);
        }
    }

    private void mudaCor(CombateAtributos atributosAlvo){
    
       
         if (atributosAlvo.getAtributos().tipo== CombateUnidade.tipoUnidade.Agressivo){
            Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color=  CorDiplomatico.color;

        }
        else if (atributosAlvo.getAtributos().tipo== CombateUnidade.tipoUnidade.Diplomatico){
            Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color= CorManipulador.color; 

                 
        }
        else  Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color= CorOfensivo.color; 

         
          
    }

    IEnumerator passaTurno()
    {
        yield return new WaitForSeconds(entreTurnos);
        if (atributosPlayer.getVidaAtual() == 0)
        {
            endBattle(true);
        }
        else if (atributosAdversario.getVidaAtual() == 0)
        {
            endBattle(false);
        }

        else
        {
            if(turnoAtual == turno.adversario)
            {
                turnoAtual = turno.jogador;
                vezDoOutro.SetActive(false);
            }
            else 
            {
                turnoAtual = turno.adversario;
                turnoAdversario();
            }
            
        }
    }
    
}
