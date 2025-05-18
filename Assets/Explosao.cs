using UnityEngine;

public class Explosao : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somExplosao;

    public AnimacaoPlayer inicio;
    public AnimacaoPlayer meio;
    public AnimacaoPlayer fim;

    public void SetActiveRenderer(AnimacaoPlayer renderer)
    {
        inicio.enabled = renderer == inicio;
        meio.enabled = renderer == meio;
        fim.enabled = renderer == fim;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        if (audioSource != null && somExplosao != null)
        {
            audioSource.PlayOneShot(somExplosao);
        }

        Destroy(gameObject, seconds);
    }
}
