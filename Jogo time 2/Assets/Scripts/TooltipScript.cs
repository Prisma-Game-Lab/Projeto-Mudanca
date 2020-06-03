using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//A: adaptado de https://www.youtube.com/watch?v=d_qk7egZ8_c
public class TooltipScript : MonoBehaviour
{
    //A: margem do fundo
    public float textPaddingSize;
    //A: deslocamento do tooltip
    public Vector2 tooltipOffset;
    //A: existe apenas 1 tooltip (singleton)
    private static TooltipScript instance;
    [SerializeField]
    //A: tooltip requer transformação de posição no jogo para posição na tela, logo precisa receber camera
    private Camera uiCamera;
    //A: texto e plano de fundo da tooltip
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        //A: inicializa instancia (singleton)
        instance = this;

        //A: encontra componentes (texto e fundo)
        //backgroundRectTransform = transform.Find("Fundo").GetComponent<RectTransform>();
        tooltipText = transform.Find("TextoTooltip").GetComponent<Text>();// this.GetComponent<Text>(); //A: alternar qual metade da linha esta comentada para mudar entre mmostrar ou esconder background quando sem texto

        ShowTooltip("");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        //A: atualiza para posição do mouse
        /*Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),Input.mousePosition,uiCamera, out localPoint);
        localPoint += tooltipOffset;
        transform.localPosition = localPoint;*/
        //A: Comentado para atender a feedbacks, mantendo texto em lugar fixo
    }
    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;

        //A: evitando ajustes no tamanho do background para fixar tooltip no lugar
        /*Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize*2, tooltipText.preferredHeight + textPaddingSize*2);
        backgroundRectTransform.sizeDelta = backgroundSize;*/
    }
    private void HideTooltip()
    {
        gameObject.SetActive(false);

    }

    public static void ExibirTooltip(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }
    public static void EsconderTooltip()
    {
        instance.HideTooltip();
    }

}