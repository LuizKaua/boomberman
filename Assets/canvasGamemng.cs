using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class canvasGamemng : MonoBehaviour
{
    public Button btnJogar;
    public Button btnSomOn;
    public Button btnSomOff;

    private bool somAtivado = true;

    void Start()
    {
        // Bot�o Jogar
        btnJogar.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Jogo");
        });

        // Bot�es de Som
        btnSomOn.onClick.AddListener(DesativarSom);
        btnSomOff.onClick.AddListener(AtivarSom);

        AtualizarBotoesSom();
    }

    public void AtivarSom()
    {
        somAtivado = true;
        AudioListener.volume = 1;
        AtualizarBotoesSom();
    }

    public void DesativarSom()
    {
        somAtivado = false;
        AudioListener.volume = 0;
        AtualizarBotoesSom();
    }

    public void AtualizarBotoesSom()
    {
        btnSomOn.gameObject.SetActive(somAtivado);
        btnSomOff.gameObject.SetActive(!somAtivado);
    }
}
