using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombaControladora : MonoBehaviour
{
    [Header("Bomba")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject bombaPrefab;
    public float bombaTempoExplosao = 3f;
    public int quantidadeBomba = 1;
    private int bombasRestantes;

    [Header("Exlosão")]
    public Explosao explosaoPrefab;
    public LayerMask explosaoLayerMask;
    public float explosaoDuracao = 1f;
    public int explosaoAlcance = 1;

    [Header("Destrutível")]
    public Tilemap destrutivelTiles;
    public Destrutível destrutívelPrefab;

    private void OnEnable()
    {
        bombasRestantes = quantidadeBomba;
    }

    private void Update()
    {
        if (bombasRestantes > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(LocalBomba());
        }
    }

    private IEnumerator LocalBomba()
    {
        Vector2 position = transform.position;
        
        GameObject bomba = Instantiate(bombaPrefab, position, Quaternion.identity);
        bombasRestantes--;

        yield return new WaitForSeconds(bombaTempoExplosao);

        position = bomba.transform.position;
        

        Explosao explosao = Instantiate(explosaoPrefab, position, Quaternion.identity);
        explosao.SetActiveRenderer(explosao.inicio);
        explosao.DestroyAfter(explosaoDuracao);
       

        Explode(position, Vector2.up, explosaoAlcance);
        Explode(position, Vector2.down, explosaoAlcance);
        Explode(position, Vector2.left, explosaoAlcance);
        Explode(position, Vector2.right, explosaoAlcance);

        Destroy(bomba);
        bombasRestantes++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length )
    {
        if(length <= 0)
        {
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosaoLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosao explosao = Instantiate(explosaoPrefab, position, Quaternion.identity);
        explosao.SetActiveRenderer(length > 1 ? explosao.meio : explosao.fim);
        explosao.SetDirection(direction);
        explosao.DestroyAfter(explosaoDuracao);
       
        Explode(position, direction, length - 1);

    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destrutivelTiles.WorldToCell(position);
        TileBase tile = destrutivelTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destrutívelPrefab, position, Quaternion.identity);
            destrutivelTiles.SetTile(cell, null);
        }
    }

    public void AdicionarBomba()
    {
        quantidadeBomba++;
        bombasRestantes++;
    }
}
