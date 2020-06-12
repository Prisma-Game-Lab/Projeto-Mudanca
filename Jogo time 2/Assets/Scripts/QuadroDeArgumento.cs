using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroDeArgumento : MonoBehaviour
{
    public Sprite iconeIncisivo,iconeDefensivo,iconeDiplomatico;

    private CombateArgumento armazenado;

    // Update is called once per frame
    void Update()
    {
        if (armazenado == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if (this.armazenado.habilidade == CombateArgumento.tipoArgumento.Ataque)
            {
                this.GetComponent<Image>().sprite = iconeIncisivo;
            }
            else if(this.armazenado.habilidade == CombateArgumento.tipoArgumento.Regenerar)
            {
                this.GetComponent<Image>().sprite = iconeDiplomatico;
            }
            else
            {
                this.GetComponent<Image>().sprite = iconeDefensivo;

            }
        }
    }

    public void CarregaArgumento(CombateArgumento novoArgumento)
    {
        armazenado = novoArgumento;
        this.GetComponent<TooltipObserver>().associaArgumento(novoArgumento);
        this.gameObject.SetActive(true);

    }
    public void LimpaArgumento()
    {
        armazenado = null;
        this.GetComponent<TooltipObserver>().associaArgumento(null);
        this.gameObject.SetActive(false);
    }
}
