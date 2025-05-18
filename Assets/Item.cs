using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somColetarItem;


    public enum ItemType 
    { 
        ExtraBomba,
        BlastRadius,
        SpeedIncrease,

    }

    public ItemType type;

    private void PegarItem(GameObject player)
    {
        if (somColetarItem != null)
        {
            AudioSource.PlayClipAtPoint(somColetarItem, transform.position, 2f);
        }

        switch (type) 
        {
            case ItemType.ExtraBomba:
                player.GetComponent<BombaControladora>().AdicionarBomba();
               
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombaControladora>().explosaoAlcance++;
               
                break;

            case ItemType.SpeedIncrease:
                
                player.GetComponent<MovimentoControlador>().speed++;
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PegarItem(other.gameObject);  
        }
    }

    
}
