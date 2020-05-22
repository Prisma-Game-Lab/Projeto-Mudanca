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

    enum alinhamento 
    {
        agressivo,
        agrematico,
        diplomatico,
        diplolador,
        manipulador
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

    [Tooltip("Objeto (Slider) que exibe o alinhamento atual")]
    public Slider sliderAlinhamento;

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
    public float velocidadeVida;
    [Tooltip("Tempo de transição entre a vez de cada um, em segundos")]
    //A: tempo aguardado entre cada turno, para o jogador compreender o que ocorre.
    public float entreTurnos;
    
    [Header("Listas dos argumentos. DEFINIR APENAS TAMANHO. ADICIONAR REFERENCIAS ADICIONA ARGUMENTOS AO INICIO DA BATALHA")]
    [Tooltip("Listas de argumentos do player")]
    //A: arrays para armazenar argumentos
    public CombateArgumento[] argumentosPlayer;
    [Tooltip("Listas de argumentos do adviersario")]
    public CombateArgumento[] argumentosAdversario;

    //A: dono da vez
    private turno turnoAtual;

    //A: armazena atributos para referencias futuras
    private CombateAtributos atributosPlayer, atributosAdversario;

    //A: contadores de quantas vezes ataques do tipo x foram usados, para definir argumento a ser criado
    private int contaAgressivosPlayer = 0, contaAgressivosAdversario = 0, contaManipuladorPlayer = 0, contaManipuladorAdversario = 0, contaDiplomaticoPlayer = 0, contaDiplomaticoAdversario = 0;

    //A: Contador de quantos argumentos cada atacante tem
    private int numArgumentosPlayer, numArgumentosAdversario;
    
    private alinhamento alinhamentoPlayer;


    void Start()
    {
        
        atributosPlayer = Player.GetComponent<CombateAtributos>();
        atributosAdversario = Adversario.GetComponent<CombateAtributos>();

        //A: inicializa alinhamento no centro
        alinhamentoPlayer = alinhamento.diplomatico;
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
        VidaPlayer.value = Mathf.Lerp(VidaPlayer.value,atributosPlayer.getVidaAtual(),velocidadeVida * Time.deltaTime);
        VidaAdversario.value = Mathf.Lerp(VidaAdversario.value,atributosAdversario.getVidaAtual(),velocidadeVida * Time.deltaTime);
        barraArgumentoPlayer.value = Mathf.Lerp(barraArgumentoPlayer.value,atributosPlayer.getArgumentos(),velocidadeVida * Time.deltaTime);
        barraArgumentoAdversario.value = Mathf.Lerp(barraArgumentoAdversario.value,atributosAdversario.getArgumentos(),velocidadeVida * Time.deltaTime);
        sliderAlinhamento.value = (int)alinhamentoPlayer;
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
                multiplicadorGolpe *= 2 + (float)bonusAlinhamento(golpe);
                
                //A: texto de feedback
                efetividade.color = Color.green;
                efetividade.text = "super efetivo!";
                Debug.Log(bonusAlinhamento(golpe));
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

        //A: ajusta multiplicador do alinhamento baseado em ataque escolhido, caso seja vez do player
        if (atributosAtacante.atributos.nome == atributosPlayer.atributos.nome)
        {
            ajustaAlinhamento(golpe);
        }
        
        //A: incrementa contador de vezes que o tipo de ataque foi usado
        somaTipo( atributosAtacante, golpe);

        //A: ajusta barra de argumentos de acordo caso nao tiver maximo de argumentos
        if ((atributosAtacante.atributos.nome == atributosPlayer.atributos.nome && numArgumentosPlayer < argumentosPlayer.Length) || (atributosAtacante.atributos.nome == atributosAdversario.atributos.nome && numArgumentosAdversario < argumentosAdversario.Length ))
        {
            atributosAtacante.setArgumentos(atributosAtacante.getArgumentos()+golpe.barraArgumento);
            if(atributosAtacante.getArgumentos()>= atributosAtacante.atributos.barraArgumento)
            {
                atributosAtacante.setArgumentos(0);
                Debug.Log("Criou Argumento");
                //A: codigo para criar argumento vai aqui;
            }
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

    //A: ajusta alinhamento conforme ataque escolhido
    private void ajustaAlinhamento(CombateAcao golpe)
    {
        switch (golpe.tipo)
        {
            case CombateAcao.tipoDano.Agressivo:
                if ((int)alinhamentoPlayer > 0) 
                {
                    alinhamentoPlayer --;
                    Debug.Log ("Agressivo --");
                }
                break;
            case CombateAcao.tipoDano.Manipulador:
            
                if ((int)alinhamentoPlayer < 4)
                {
                    alinhamentoPlayer ++;
                    Debug.Log ("Manipulador ++");
                }
                break;
            case CombateAcao.tipoDano.Diplomatico:
                if((int)alinhamentoPlayer < 2) 
                {
                    alinhamentoPlayer++;
                    Debug.Log ("Diplomatico ++");
                }
                else if ((int)alinhamentoPlayer > 2)
                {
                    alinhamentoPlayer--;
                    
                    Debug.Log ("Diplomatico --");
                }
                break;
            default:
                return;
        }
    }

    //A: calcula dano bonus baseado no alinhamento
    private double bonusAlinhamento(CombateAcao golpe)
    {
        switch (golpe.tipo)
        {
            case CombateAcao.tipoDano.Agressivo:
                if ((int)alinhamentoPlayer == 0) return 1;
                else if ((int)alinhamentoPlayer == 0) return 0.5;
                else return 0;
            case CombateAcao.tipoDano.Manipulador:
                if ((int)alinhamentoPlayer == 4)return 1;
                else if ((int)alinhamentoPlayer == 3) return 0.5;
                else return 0;
            case CombateAcao.tipoDano.Diplomatico:
                if ((int)alinhamentoPlayer == 2)return 1;
                else if ((int)alinhamentoPlayer == 1 || (int)alinhamentoPlayer == 3) return 0.5;
                else return 0;
            default:
                return 0;
        }

    }

    //A: decide a qual contador somar +1 
    private void somaTipo(CombateAtributos atributosAtacante,CombateAcao golpe)
    {
        //A: se jogador foi o atacante
        if(atributosAtacante.atributos.nome == atributosPlayer.atributos.nome)
        {
            switch(golpe.tipo)
            {
            case CombateAcao.tipoDano.Agressivo:
            
                contaAgressivosPlayer++;
                break;
                
            case CombateAcao.tipoDano.Manipulador:
                
                contaManipuladorPlayer++;
                break;
                
            case CombateAcao.tipoDano.Diplomatico:

                contaDiplomaticoPlayer++;
                break;  
                
            default:
                return;
            }
        }
        else
        {
            switch(golpe.tipo)
            {
            case CombateAcao.tipoDano.Agressivo:
                
                contaAgressivosAdversario++;
                break;
                
            case CombateAcao.tipoDano.Manipulador:
                
                contaManipuladorAdversario++;
                break;
                
            case CombateAcao.tipoDano.Diplomatico:
                
                contaDiplomaticoAdversario++;
                break;
                
            default:
                return;
            }
        }
    }

    //A: calcula qual dos tipos foi o maior contador para escolher argumento a ser criado: 0 se agressivo, 1 se manipulador e 2 se diplomatico
    private int tipoMaisUsado(CombateAtributos atributosAtacante)
    {
        //A: se jogador for o atacante
        if(atributosAtacante.atributos.nome == atributosPlayer.atributos.nome)
        {
            if (contaAgressivosPlayer > contaManipuladorPlayer && contaAgressivosPlayer > contaDiplomaticoPlayer)
                return 0;
            else if (contaManipuladorPlayer > contaAgressivosPlayer && contaManipuladorPlayer > contaDiplomaticoPlayer)
                return 1;
            else return 2;
        }
        else
        {
            if (contaAgressivosAdversario > contaManipuladorAdversario && contaAgressivosAdversario > contaDiplomaticoAdversario)
                return 0;
            else if (contaManipuladorAdversario > contaAgressivosAdversario && contaManipuladorAdversario > contaDiplomaticoAdversario)
                return 1;
            else return 2;
        }
    }


    //A: gerencia variaveis relacionadas a passagem de turno
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
