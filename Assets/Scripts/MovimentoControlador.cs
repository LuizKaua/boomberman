using System.Runtime.CompilerServices;
using UnityEngine;

public class MovimentoControlador : MonoBehaviour
{
   public new Rigidbody2D rigidbody2D { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;

    public AnimacaoPlayer animacaoSubir;
    public AnimacaoPlayer animacaoDescer;
    public AnimacaoPlayer animacaoVirarEsquerda;
    public AnimacaoPlayer animacaoVirarDireita;
    public AnimacaoPlayer animacaoMorte;
    private AnimacaoPlayer ativarAnimacao;

   
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ativarAnimacao = animacaoDescer;
    }

    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, animacaoSubir);
        }
        else if(Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, animacaoDescer);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, animacaoVirarEsquerda);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, animacaoVirarDireita);

        }
        else
        {
            SetDirection(Vector2.zero, ativarAnimacao);

        }

    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody2D.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimacaoPlayer animacaoPlayer)
    {
        direction = newDirection;

        animacaoSubir.enabled = animacaoPlayer == animacaoSubir;
        animacaoDescer.enabled = animacaoPlayer == animacaoDescer;
        animacaoVirarEsquerda.enabled = animacaoPlayer == animacaoVirarEsquerda;
        animacaoVirarDireita.enabled = animacaoPlayer == animacaoVirarDireita;

        ativarAnimacao = animacaoPlayer;
        ativarAnimacao.idle = direction == Vector2.zero;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosao"))
        {
            MorrerSequencialmente();
        }
    }

    private void MorrerSequencialmente()
    {
        enabled = false;
        GetComponent<BombaControladora>().enabled = false;

        animacaoSubir.enabled = false;
        animacaoDescer.enabled = false;
        animacaoVirarEsquerda.enabled = false;
        animacaoVirarDireita.enabled = false;
        animacaoMorte.enabled = true;

        Invoke(nameof(FimSequenciaMorte), 1.25f);
    }

    private void FimSequenciaMorte()
    {
        gameObject.SetActive(false);
        FindAnyObjectByType<GameManager>().CheckWinState();
    }
}
