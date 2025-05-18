using UnityEngine;

public class Destrutível : MonoBehaviour
{
    public float tempoDestruicao = 1f;

    [Range (0f, 1f)]
    public float gerarItem = 0.2f;
    public GameObject[] ItemEspaunavel;

    private void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }

    private void OnDestroy()
    {
        if(ItemEspaunavel.Length > 0 && Random.value < gerarItem)
        {
            int randomIndex = Random.Range(0, ItemEspaunavel.Length);
            Instantiate(ItemEspaunavel[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
