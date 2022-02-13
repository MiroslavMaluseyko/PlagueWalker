
using System.Collections;
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    //is obstacle plagued or not
    [SerializeField] private bool plagued;
    //renderer for color changing
    [SerializeField] private new Renderer renderer;
    
    //colors for healthy and plagued object
    [SerializeField] private Color normalColor;
    [SerializeField] private Color plagueColor;

    //effect after death
    [SerializeField] private ParticleSystem deathEffect;

    public void SetPlague(bool isPlagued)
    {
        plagued = isPlagued;
        renderer.material.color = plagued ? plagueColor : normalColor;
        if (isPlagued)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(.1f);
        AudioManager.Instance.Play("ObstacleExplosion");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
