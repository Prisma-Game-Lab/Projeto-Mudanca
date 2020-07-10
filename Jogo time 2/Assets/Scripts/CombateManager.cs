using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    /*[Tooltip("Cores que representam posturas")]
    public Material CorDiplomatico,CorManipulador,CorOfensivo;
    private Color CorAtual;*/

    //A: referencia as barras de vida e velocidade que descem
    [Tooltip("Objetos (Sliders) das barras de vida e de argumentos")]
    public Slider VidaPlayer, VidaAdversario, barraArgumentoPlayer, barraArgumentoAdversario;

    [Tooltip("Objeto (Slider) que exibe o alinhamento atual")]
    public Slider sliderAlinhamento;

    [Tooltip("Indicadores de mudanças do alinhamento")]
    public GameObject IdxAlinhaEsquerda,IdxAlinhaDireita,IdxAlinhaDiplomatico,IdxAlinhaAgressivo,IdxAlinhaManipulador,IdxAlinhaDirRev, IdxAlinhaEsqRev;

    [Tooltip("Objeto LevelManager para executar transição entre cenas")]
    //A: Telas Placeholder de vitoria e derrota
    public SceneControl LevelManagerObject;

    [Tooltip("Referências dos textos na UI")]
    //A: nomes na UI do jogador e do adversario
    public Text vidaPlayerTexto, vidaAdversarioTexto;

    [Tooltip("Textos dos nomes de cada ação que o jogador pode usar")]
    //A: nomes das ações do jogador
    public Text[] nomeAcoes;
    [Tooltip("Texto indicando efetividade da ação escolhida")]
    //A: Texto que da feedback se ataque foi efetivo ou não
    public Text efetividade;

    [Tooltip("Texto de falas dos personagens conforme a ação utilizada")]
    public GameObject falasPlayer,falasAdversario;

    [Tooltip("Objetos que exibirão argumentos do player")]
    public GameObject[] quadroArgumentoPlayer;

    [Tooltip("Objetos que exibirão argumentos do adversario")]
    public GameObject[] quadroArgumentoAdversario;

    [Header("Ajustes Finos para os GDs")]
    
    [Tooltip("Velocidade com que a vida desce")]
    public float velocidadeVida;
    [Tooltip("Tempo de transição entre a vez de cada um, em segundos")]
    //A: tempo aguardado entre cada turno, para o jogador compreender o que ocorre.
    public float entreTurnos;
    [Tooltip("Multiplica o dano causado quando o ataque é efetivo")]
    public float multiplicadorEfetivo;
    [Tooltip("Multiplica o dano causado quando o ataque não é efetivo")]
    public float multiplicadorResistente;
    [Tooltip("Valor que multiplica o dano quando a postura é 'Defensivo Fortifica'")]
    public float multiplicadorFortalecimento;
    [Header("Cenas pos batalha")]
    [Tooltip("Nome da cena a carregar caso o jogador vença")]
    public string cenaVitoria;
    [Tooltip("Nome da cena a carregar caso o jogador perca")]
    public string cenaDerrota;
    //A: arrays para armazenar argumentos
    private CombateArgumento[] argumentosPlayer;
    private CombateArgumento[] argumentosAdversario;

    //A: Valores pertinentes aos argumentos. Não funcionam dentro da classe CombateArgumento. Talvez seja coisa de scriptable object
    private int[] vidaArgumentosPlayer;
    private int[] turnoArgumentosPlayer;
    private int[] vidaArgumentosAdversario;
    private int[] turnoArgumentosAdversario;

    //A: Armazena gerenciador de fase de batalha
    private ModificadorAtributos gerenteFase;

    //A: dono da vez
    private turno turnoAtual;

    //A: armazena atributos para referencias futuras
    private CombateAtributos atributosPlayer, atributosAdversario;

    //A: contadores de quantas vezes ataques do tipo x foram usados, para definir argumento a ser criado
    private int contaAgressivosPlayer = 0, contaAgressivosAdversario = 0, contaManipuladorPlayer = 0, contaManipuladorAdversario = 0, contaDiplomaticoPlayer = 0, contaDiplomaticoAdversario = 0;

    //A: Contador de quantos argumentos cada atacante tem
    private int numArgumentosPlayer, numArgumentosAdversario;
    
    private alinhamento alinhamentoPlayer;

    private string resposta;

    public Animator PlayerBattle;
    public Animator EnemyBattle;
    public Animator BrunoBattle;

    public AudioSource atacAudio;


    void Start()
    {

        LevelManagerObject= FindObjectOfType<SceneControl>();
        atributosPlayer = Player.GetComponent<CombateAtributos>();
        atributosAdversario = Adversario.GetComponent<CombateAtributos>();
        gerenteFase = Adversario.GetComponent<ModificadorAtributos>();

        //A: inicializa alinhamento no centro
        alinhamentoPlayer = alinhamento.diplomatico;
        efetividade.text = ""; 

        //A: aloca numero maximo de argumentos igual a numero de quadros de exibicao
        argumentosPlayer = new CombateArgumento[quadroArgumentoPlayer.Length];
        argumentosAdversario = new CombateArgumento[quadroArgumentoAdversario.Length];
        vidaArgumentosPlayer = new int[quadroArgumentoPlayer.Length];
        vidaArgumentosAdversario = new int[quadroArgumentoAdversario.Length];
        turnoArgumentosPlayer = new int[quadroArgumentoPlayer.Length];
        turnoArgumentosAdversario = new int[quadroArgumentoAdversario.Length];
        
        for(int i=0;i<argumentosPlayer.Length;i++)
        {
            argumentosPlayer[i] = null;
            vidaArgumentosPlayer[i] = 0;
            turnoArgumentosPlayer[i] = 9999;
        }
        for(int i=0;i<argumentosAdversario.Length;i++)
        {
            argumentosAdversario[i] = null;
            vidaArgumentosAdversario[i] = 0;
            turnoArgumentosAdversario[i] = 9999;
        }

        atributosPlayer.Shuffle();
        //A: Cada ação do jogador substitui o nome escrito em um dos botoes.
        for(int i=0; i< nomeAcoes.Length;i++)
        {
            //escolhe nome aleatorio entre os descritos no array
            nomeAcoes[i].text = atributosPlayer.getAcao(i).nome[0];
            //nomeAcoes[i].GetComponent<TooltipObserver>().associaAcao(atributosPlayer.getAcao(i));

            //A: Antigo
            //nomeAcoes[i].text = atributosPlayer.getAcao(i).nome[Random.Range(0,atributosPlayer.getAcao(i).nome.Length)];
            //nomeAcoes[i].GetComponent<TooltipObserver>().associaAcao(atributosPlayer.getAcao(i));
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
        //A: garante que nenhuma seta aparece inicialmente no alinhamento
        IdxAlinhaAgressivo.SetActive(false);
        IdxAlinhaDiplomatico.SetActive(false);
        IdxAlinhaDireita.SetActive(false);
        IdxAlinhaEsquerda.SetActive(false);
        IdxAlinhaManipulador.SetActive(false);
        IdxAlinhaDirRev.SetActive(false);
        IdxAlinhaEsqRev.SetActive(false);
    }

    void Update()
    {
        atributosPlayer.setFase(gerenteFase.getFase());
        atributosAdversario.setFase(gerenteFase.getFase());

        //for(int i=0;i<nomeAcoes.Length;i++)
        //{
        //    nomeAcoes[i].GetComponent<TooltipObserver>().associaAcao(atributosPlayer.getAcao(i));
        //}
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
        resposta = atributosPlayer.getAcao(0).respostas[0];
        aplicaDano(atributosPlayer,atributosPlayer.getAcao(0),atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    public void Acao2()
    {
        resposta = atributosPlayer.getAcao(1).respostas[0];
        aplicaDano(atributosPlayer,atributosPlayer.getAcao(1),atributosAdversario);
        vezDoOutro.SetActive(true);
        StartCoroutine (passaTurno());
    }

    public void Acao3()
    {
        resposta = atributosPlayer.getAcao(2).respostas[0];
        aplicaDano(atributosPlayer,atributosPlayer.getAcao(2),atributosAdversario);
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

        if (golpe.tipo != CombateAcao.tipoDano.Neutro)
        {
            //A: ajusta multiplicador conforme tipo
            if(atributosAlvo.isVulneravel((int)golpe.tipo))
            {
                multiplicadorGolpe *= multiplicadorEfetivo + (float)bonusAlinhamento(golpe);
                
                //A: texto de feedback
                efetividade.color = Color.green;
                efetividade.text = "super efetivo!";
            }
            else if(atributosAlvo.isResistente((int)golpe.tipo))
            {
                multiplicadorGolpe = multiplicadorGolpe * multiplicadorResistente;

                //A: texto de feedback
                efetividade.color = Color.red;
                efetividade.text = "pouco efetivo...";
            }
        }
        //A: implementacao da postura Defensivo Fortifica, aumentando o dano
        if(atributosAlvo.atributos.postura == CombateUnidade.posturaUnidade.defensivoFortalece)
        {
            multiplicadorGolpe *= multiplicadorFortalecimento;
            atributosAdversario.setAuxiliar(0);
        }
        danoResultante = (int) ((ataque+danoBonusArgumento(atributosAtacante))*multiplicadorGolpe-(atributosAlvo.atributos.defesa + defesaBonusArgumento(atributosAlvo)));

        //EnemyBattle.SetTrigger("GetHit");
        atacAudio.Play();

        if (atributosAtacante == atributosPlayer)
        {
            EnemyBattle.SetTrigger("GetHit");
            BrunoBattle.SetTrigger("Porrada");

        }
        else PlayerBattle.SetTrigger("Dano");

        //A: implementação da postura Reage a Agressivo
        if (atributosAlvo.atributos.postura == CombateUnidade.posturaUnidade.reageAgressivo && golpe.tipo == CombateAcao.tipoDano.Agressivo)
        {
            aplicaDano(atributosAlvo, atributosAlvo.getAcao(0), atributosAtacante);
        }

        //A: implementação da postura Defensivo Fortalece, salva dano levado
        if(atributosAlvo.atributos.postura == CombateUnidade.posturaUnidade.defensivoFortalece && golpe.tipo == CombateAcao.tipoDano.Manipulador)
        {
            atributosAdversario.setAuxiliar(atributosAdversario.getAuxiliar()+1);
        }

        //A: Garante dano minimo = 1
        if(danoResultante < 1)
        {
            danoResultante = 1;
        }
        
        //A: implementação da postura ignoraManipulador
        if(atributosAlvo.atributos.postura == CombateUnidade.posturaUnidade.ignoraManipulador && golpe.tipo == CombateAcao.tipoDano.Manipulador)
        {
            danoResultante = 0;
            
            efetividade.color = Color.blue;
            efetividade.text = "imune";
        }

        //A: implementação de argumentos de roubo de vida e evasão
        for(int i=0;i<argumentosPlayer.Length;i++)
        {
            if(argumentosPlayer[i] != null)
            {
                if(argumentosPlayer[i].habilidade == CombateArgumento.tipoArgumento.RoubaVida && atributosAtacante == atributosPlayer)
                {
                    atributosPlayer.curar((int)argumentosPlayer[i].valor * danoResultante);
                }
                else if(argumentosPlayer[i].habilidade == CombateArgumento.tipoArgumento.Evasao && atributosAlvo == atributosPlayer)
                {
                    danoResultante = 0;
                    argumentosPlayer[i].valor --;
                    if(argumentosPlayer[i].valor <1)
                    {
                        quadroArgumentoAdversario[i].GetComponent<QuadroDeArgumento>().LimpaArgumento();
                    }
                }
            }
        }
        for(int i=0;i<argumentosAdversario.Length;i++)
        {
            if(argumentosAdversario[i] != null)
            {
                if(argumentosAdversario[i].habilidade == CombateArgumento.tipoArgumento.RoubaVida && atributosAtacante == atributosAdversario)
                {
                    atributosAdversario.curar((int)argumentosAdversario[i].valor * danoResultante);
                }
                else if(argumentosAdversario[i].habilidade == CombateArgumento.tipoArgumento.Evasao && atributosAlvo == atributosAdversario)
                {
                    danoResultante = 0;
                    efetividade.color = Color.blue;
                    efetividade.text = "imune";
                    argumentosAdversario[i].valor --;
                    if(argumentosAdversario[i].valor <1)
                    {
                        quadroArgumentoAdversario[i].GetComponent<QuadroDeArgumento>().LimpaArgumento();
                    }
                }
            }
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
        if ((atributosAtacante.atributos.nome == atributosPlayer.atributos.nome && numArgumentosPlayer < argumentosPlayer.Length && turnoAtual == turno.jogador) || (atributosAtacante.atributos.nome == atributosAdversario.atributos.nome && numArgumentosAdversario < argumentosAdversario.Length && turnoAtual == turno.adversario))
        {
            atributosAtacante.setArgumentos(atributosAtacante.getArgumentos()+golpe.barraArgumento);
        }

        if(turnoAtual==turno.adversario)
        {
            falasAdversario.transform.GetChild(1).GetComponent<Text>().text = golpe.nome[0];
            falasAdversario.SetActive(true);
        }
        else if(turnoAtual==turno.jogador && atributosAtacante == atributosPlayer)
        {
            falasPlayer.transform.GetChild(1).GetComponent<Text>().text = golpe.nome[0];
            falasPlayer.SetActive(true);
        }
    }

    private void turnoAdversario()
    {
        //A: implementação da postura Ataque Esmagador
        if(atributosAdversario.atributos.postura == CombateUnidade.posturaUnidade.golpeEsmagador && atributosAdversario.getAuxiliar() < 2)
        {
            efetividade.color = Color.yellow;
            efetividade.text = "ALERTA!!!";
            atributosAdversario.setAuxiliar(atributosAdversario.getAuxiliar()+1);
        }
        else if (atributosAdversario.atributos.postura == CombateUnidade.posturaUnidade.golpeEsmagador && atributosAdversario.getAuxiliar() >= 2)
        {
            resposta = atributosAdversario.getAcao(0).respostas[0];
            aplicaDano(atributosAdversario,atributosAdversario.getAcao(0),atributosPlayer);
            atributosAdversario.setAuxiliar(0);
        }
        //A: turno comum do adversario
        else
        {   
            int ataqueEscolhido = Random.Range(0, atributosAdversario.getLengthAcoes());
            resposta = atributosAdversario.getAcao(ataqueEscolhido).respostas[0];
            aplicaDano(atributosAdversario,atributosAdversario.getAcao(ataqueEscolhido),atributosPlayer);
        }
        StartCoroutine(passaTurno());
    }

    private void endBattle(bool defeat)
    {
        if (defeat)
        {
             LevelManagerObject.GetComponent<SceneControl>().LoadScene("Quarto");
          
        }
        else{
           
             LevelManagerObject.GetComponent<SceneControl>().LoadScene("QuartoVitoriaDia1");
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
                    IdxAlinhaEsquerda.SetActive(true);
                    alinhamentoPlayer --;
                }
                
                IdxAlinhaAgressivo.SetActive(true);
                break;
            case CombateAcao.tipoDano.Manipulador:
            
                if ((int)alinhamentoPlayer < 4)
                {
                    IdxAlinhaDireita.SetActive(true);
                    alinhamentoPlayer ++;
                }
                IdxAlinhaManipulador.SetActive(true);
                break;
            case CombateAcao.tipoDano.Diplomatico:
                if((int)alinhamentoPlayer < 2) 
                {
                    IdxAlinhaEsqRev.SetActive(true);
                    alinhamentoPlayer++;
                }
                else if ((int)alinhamentoPlayer > 2)
                {
                    IdxAlinhaDirRev.SetActive(true);
                    alinhamentoPlayer--;
                }
                IdxAlinhaDiplomatico.SetActive(true);
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

    private void resetaContadores(CombateAtributos atributosAtacante)
    {
        if(atributosAtacante.atributos.nome == atributosPlayer.atributos.nome)
        {
            contaAgressivosPlayer = 0;
            contaManipuladorPlayer = 0;
            contaDiplomaticoPlayer = 0;
        }
        else
        {
            contaAgressivosAdversario = 0;
            contaManipuladorAdversario = 0;
            contaDiplomaticoAdversario = 0;
        }
    }

    private void criaArgumento(CombateAtributos donoDoArg)
    {
        CombateArgumento argCriado;

        //A: Escolhe argumento a ser criado
        switch(tipoMaisUsado(donoDoArg))
        {
            case 0:
                //Agressivo
                argCriado = donoDoArg.getArgAgre();
                break;
            case 1:
                //Defensivo
                argCriado = donoDoArg.getArgDef();
                break;
            default:
                //Diplomatico
                argCriado = donoDoArg.getArgDipl();
                break;
        }

        //A: Adiciona argumento criado a primeiro espaço vazio da lista de argumentos
        if(donoDoArg == atributosPlayer)
        {
            for(int i=0;i<argumentosPlayer.Length;i++)
            {
                if(argumentosPlayer[i] == null)
                {
                    argumentosPlayer[i] = argCriado;
                    vidaArgumentosPlayer[i] = argCriado.vida;
                    turnoArgumentosPlayer[i] = 0;
                    quadroArgumentoPlayer[i].GetComponent<QuadroDeArgumento>().CarregaArgumento(argCriado);
                    numArgumentosPlayer++;
                    break;
                }
            }
        }
        else
        {
            for(int i=0;i<argumentosAdversario.Length;i++)
            {
                if(argumentosAdversario[i] == null)
                {
                    argumentosAdversario[i] = argCriado;
                    vidaArgumentosAdversario[i] = argCriado.vida;
                    turnoArgumentosAdversario[i] = 0;
                    quadroArgumentoAdversario[i].GetComponent<QuadroDeArgumento>().CarregaArgumento(argCriado);
                    numArgumentosAdversario++;
                    break;
                }
            }
        }
        resetaContadores(donoDoArg);
    }

    private int danoBonusArgumento(CombateAtributos atributosAtacante)
    {
        int bonusTotal = 0;
        if(atributosAtacante == atributosPlayer)
        {
            for(int i=0; i<argumentosPlayer.Length;i++)
            {
                if(argumentosPlayer[i] != null && argumentosPlayer[i].habilidade == CombateArgumento.tipoArgumento.Ataque)
                {
                    bonusTotal += (int)argumentosPlayer[i].valor;
                }
            }
        }
        else
        {
            for(int i=0; i<argumentosPlayer.Length;i++)
            {
                if(argumentosAdversario[i] != null && argumentosAdversario[i].habilidade == CombateArgumento.tipoArgumento.Ataque)
                {
                    bonusTotal += (int)argumentosAdversario[i].valor;
                }
            }
        }
        return bonusTotal;
    }

    private int defesaBonusArgumento(CombateAtributos atributosAtacante)
    {
        int bonusTotal = 0;
        if(atributosAtacante == atributosPlayer)
        {
            for(int i=0; i<argumentosPlayer.Length;i++)
            {
                if(argumentosPlayer[i] != null && argumentosPlayer[i].habilidade == CombateArgumento.tipoArgumento.Defesa)
                {
                    bonusTotal += (int)argumentosPlayer[i].valor;
                }
            }
        }
        else
        {
            for(int i=0; i<argumentosPlayer.Length;i++)
            {
                if(argumentosAdversario[i] != null && argumentosAdversario[i].habilidade == CombateArgumento.tipoArgumento.Defesa)
                {
                    bonusTotal += (int)argumentosAdversario[i].valor;
                }
            }
        }
        return bonusTotal;
    }


    public void BotaoVitoria()
    {
        SceneManager.LoadScene(cenaVitoria);
    }

    public void BotaoDerrota()
    {
        SceneManager.LoadScene(cenaDerrota);
    }
    //A: gerencia variaveis relacionadas a passagem de turno
    IEnumerator passaTurno()
    {
        yield return new WaitForSeconds(entreTurnos/2);

        //A: gerencia criacao de argumentos de ambos (pois existem posturas com retorno de dano, etc)
        if(atributosAdversario.getArgumentos()>= atributosAdversario.atributos.barraArgumento)
        {
            atributosAdversario.setArgumentos(0);
            criaArgumento(atributosAdversario);
            //A: codigo para criar argumento para adversario vai aqui;
        }
        if(atributosPlayer.getArgumentos()>= atributosPlayer.atributos.barraArgumento)
        {
            atributosPlayer.setArgumentos(0);
            criaArgumento(atributosPlayer);
            //A: codigo para criar argumento para jogador vai aqui;
        }

        //A: ajusta indices do alinhamento
        IdxAlinhaAgressivo.SetActive(false);
        IdxAlinhaDiplomatico.SetActive(false);
        IdxAlinhaDireita.SetActive(false);
        IdxAlinhaEsquerda.SetActive(false);
        IdxAlinhaManipulador.SetActive(false);
        IdxAlinhaDirRev.SetActive(false);
        IdxAlinhaEsqRev.SetActive(false);

        //A: esconde fala e exibe resposta
        if(turnoAtual==turno.adversario)
        {
            falasAdversario.SetActive(false);
            if(resposta != "")
            {
                falasPlayer.transform.GetChild(1).GetComponent<Text>().text = resposta;
                falasPlayer.SetActive(true);
            }
        }
        else
        {
            falasPlayer.SetActive(false);

            if(resposta != "")
            {
                falasAdversario.transform.GetChild(1).GetComponent<Text>().text = resposta;
                falasAdversario.SetActive(true);
            }
        }

        yield return new WaitForSeconds(entreTurnos/2);
               
        falasAdversario.SetActive(false);
        falasPlayer.SetActive(false);

        for(int i=0;i<argumentosPlayer.Length;i++)
        {
            if(argumentosPlayer[i] != null)
            {
                turnoArgumentosPlayer[i]++;

                if(vidaArgumentosPlayer[i] <= 0 || turnoArgumentosPlayer[i]/2 >= argumentosPlayer[i].duracao)
                {

                    argumentosPlayer[i] = null;
                    quadroArgumentoPlayer[i].GetComponent<QuadroDeArgumento>().LimpaArgumento();
                    numArgumentosPlayer --;
                }
                else
                {
                    if(argumentosPlayer[i].habilidade == CombateArgumento.tipoArgumento.Regenerar && turnoAtual == turno.jogador)
                    {
                        atributosPlayer.curar((int)argumentosPlayer[i].valor);
                    }
                }
            }
        }
        for(int i=0;i<argumentosAdversario.Length;i++)
        {
            if(argumentosAdversario[i] != null)
            {
            
                turnoArgumentosAdversario[i]++;
                
                if(vidaArgumentosAdversario[i] <= 0 || turnoArgumentosAdversario[i]/2 >= argumentosAdversario[i].duracao)
                {
                    argumentosAdversario[i] = null;
                    quadroArgumentoAdversario[i].GetComponent<QuadroDeArgumento>().LimpaArgumento();
                    numArgumentosAdversario --;
                }
                else
                {
                    if(argumentosAdversario[i].habilidade == CombateArgumento.tipoArgumento.Regenerar && turnoAtual == turno.adversario)
                    {
                        atributosAdversario.curar((int)argumentosAdversario[i].valor);
                    }
                }
            }
        }

        yield return new WaitForSeconds(entreTurnos/7);

        resposta = "";
        //A: Verifica se combate acabou
        efetividade.text = "";
       
        if (atributosPlayer.getVidaAtual() == 0)
        {
            LevelManagerObject.derrota=true;
            endBattle( LevelManagerObject.derrota);
        }
        else if (atributosAdversario.getVidaAtual() == 0)
        {   LevelManagerObject.derrota=false;
            endBattle(LevelManagerObject.derrota);
        }
        //A: Se nao acabou, passa vez
        else
        {
            if(turnoAtual == turno.adversario)
            {
                turnoAtual = turno.jogador;
                atributosPlayer.Shuffle();
                for(int i=0; i< nomeAcoes.Length;i++)
                {
                    //escolhe nome aleatorio entre os descritos no array
                    nomeAcoes[i].text = atributosPlayer.getAcao(i).nome[0];
                    //nomeAcoes[i].GetComponent<TooltipObserver>().associaAcao(atributosPlayer.getAcao(i));
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
