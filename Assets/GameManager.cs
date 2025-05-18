using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinState()
    {
        int contagemVivo = 0;

        foreach (GameObject player in players) 
        {
            if (player.activeSelf)
            {
                contagemVivo++;
            }
        }

        if(contagemVivo <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
