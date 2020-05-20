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

    [Header("Referências do Unity. Cuidado ao mexer")]
    [Tooltip("GameObjects do jogador e do adversário")]
    //A:referencias dos objetos de jogador e adversario na cena
    public GameObject Player;
    [Tooltip("GameObjects do jogador e do adversário")]
    public GameObject Adversario;
    //A: referencia de objeto na UI para impedir jogador de apertar botoes na vez do adversario
    [Tooltip("Objeto que impede jogador de jogar na vez do adversário")]
    public GameObject vezDoOutro;

    [Tooltip("Cores que representam posturas")]
    public Material CorDiplomatico,CorManipulador,CorOfensivo;
    private Color CorAtual;

    

    //A: referencia as barras de vida e velocidade que descem
    [Tooltip("Objetos (Sliders) das barras de vida e de argumentos")]
    public Slider VidaPlayer, VidaAdversario, barraArgumentoPlayer, barraArgumentoAdversario;

    [Tooltip("Paineis de vitória e derrota")]
    //A: Telas Placeholder de vitoria e derrota
    public GameObject TelaVitoria, TelaDerrota;

    [Tooltip("Referências dos textos na UI")]
    //A: nomes na UI do jogador e do adversario
    public Text nomePlayer, nomeAdversario, vidaPlayerTexto, vidaAdversarioTexto;

    [Tooltip("Textos dos nomes de cada ação que o jogador pode usar")]
    //A: nomes das ações do jogador
    public Text[] nomeAcoes;
    [Tooltip("Texto indicando efetividade da ação escolhida")]
    //A: Texto que da feedback se ataque foi efetivo ou não
    public Text efetividade;

    [Header("Ajustes de tempo")]
    
    [Tooltip("Velocidade com que a vida desce")]
    public float VelocidadeVida;
    [Tooltip("Tempo de transição entre a vez de cada um, em segundos")]
    //A: tempo aguardado entre cada turno, para o jogador compreender o que ocorre.
    public float entreTurnos;
    
    //A: dono da vez
    private turno turnoAtual;

    //A: armazena atributos para referencias futuras
    private CombateAtributos atributosPlayer, atributosAdversario;

    


    void Start()
    {
        
        atributosPlayer = Player.GetComponent<CombateAtributos>();
        atributosAdversario = Adversario.GetComponent<CombateAtributos>();

        //A: Atualiza nomes de player e adversario na hud
        nomePlayer.text = atributosPlayer.atributos.nome;
        nomeAdversario.text = atributosAdversario.atributos.nome;
        efetividade.text = ""; 

        //A: Cada ação do jogador substitui o nome escrito em um dos botoes.
        for(int i=0; i< nomeAcoes.Length;i++)
        {
            //escolhe nome aleatorio entre os descritos no array
            nomeAcoes[i].text = atributosPlayer.acoes[i].nome[Random.Range(0,atributosPlayer.acoes[i].nome.Length)];
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
        barraArgumentoPlayer.value = Mathf.Lerp(barraArgumentoPlayer.value,atributosPlayer.getArgumentos(),VelocidadeVida * Time.deltaTime);
        barraArgumentoAdversario.value = Mathf.Lerp(barraArgumentoAdversario.value,atributosAdversario.getArgumentos(),VelocidadeVida * Time.deltaTime);
        //CorAtual= Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        
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
        //mudaCor(atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    private void aplicaDano (CombateAtributos atributosAtacante,CombateAcao golpe, CombateAtributos atributosAlvo)
    {
        int ataque = atributosAtacante.atributos.dano;
        float multiplicadorGolpe = golpe.dano/100;
        int danoResultante;

        if(golpe.tipo != CombateAcao.tipoDano.Neutro)
        {
            //A: ajusta multiplicador conforme tipo
            if(atributosAlvo.isVulneravel((int)golpe.tipo))
            {
                multiplicadorGolpe *= 2;
                
                //A: texto de feedback
                efetividade.color = Color.green;
                efetividade.text = "super efetivo!";
            }
            else if(atributosAlvo.isResistente((int)golpe.tipo))
            {
                multiplicadorGolpe = multiplicadorGolpe/2;

                //A: texto de feedback
                efetividade.color = Color.red;
                efetividade.text = "pouco efetivo...";
            }
        }
        danoResultante = (int) (ataque*multiplicadorGolpe-atributosAlvo.atributos.defesa);

        //A: Garante dano minimo = 1
        if(danoResultante <=0)
        {
            danoResultante = 1;
        }
        atributosAlvo.danifica(danoResultante);

        //A: ajusta barra de argumentos de acordo
        atributosAtacante.setArgumentos(atributosAtacante.getArgumentos()+golpe.barraArgumento);
        if(atributosAtacante.getArgumentos()>= atributosAtacante.atributos.barraArgumento)
        {
            atributosAtacante.setArgumentos(0);
            Debug.Log("Criou Argumento");
            //A: codigo para criar argumento vai aqui;
        }
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

    /*private void mudaCor(CombateAtributos atributosAlvo){
    
       
         if (atributosAlvo.getAtributos().tipo== CombateUnidade.tipoUnidade.Agressivo){
            Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color=  CorOfensivo.color;

        }
        else if (atributosAlvo.getAtributos().tipo== CombateUnidade.tipoUnidade.Diplomatico){
            Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color= CorDiplomatico.color;

                 
        }
        else  Adversario.transform.GetChild(0).GetComponent<SpriteRenderer>().color= CorManipulador.color;

         
          
    }*/

    IEnumerator passaTurno()
    {
        yield return new WaitForSeconds(entreTurnos);
        efetividade.text = "";
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
                for(int i=0; i< nomeAcoes.Length;i++)
                {
                //escolhe nome aleatorio entre os descritos no array
                    nomeAcoes[i].text = atributosPlayer.acoes[i].nome[Random.Range(0,atributosPlayer.acoes[i].nome.Length)];
                }
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
