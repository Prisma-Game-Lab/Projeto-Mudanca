using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{
    [Header("Referências do Unity. Cuidado ao mexer")]
    [Tooltip("alvo que a camera segue (Prefere-se que seja o jogador)")]
    public GameObject Alvo;
    [Tooltip("Velocidade de movimento da camera. Quanto mais próxima à do alvo, mais centralizado ele fica. Deve ser positiva")]
    public float moveSpeed;

    [Tooltip("Posição relativa entre camera e alvo. Mantenha um Z negativo")]
    public Vector3 offset;
    private Transform transfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        transfPlayer = Alvo.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //A: move-se junto com o player 
        transform.position = Vector3.Lerp(transform.position, transfPlayer.position + new Vector3(0,0,-10), moveSpeed * Time.deltaTime);
    }
}
