
using System.Linq;
using UnityEngine;

public class ExplosiveObj : MonoBehaviour
{
    //explosion radius when charge is 1
    [SerializeField] private float explosionRadius;
    //objects that are affected by explosion
    [SerializeField] private LayerMask layerMask;
    
    //center of explosion
    private Vector3 origin;
    //objects that were touched by the explosion
    private Collider[] victims;
    
    public void Explode()
    {
        //multiplier according to charge
        float scaler = GetComponent<ChargeableObj>().currCharge;
        
        //boom
        origin = transform.position;
        victims = Physics.OverlapSphere(origin, explosionRadius*scaler, layerMask);
        
        //Bring plague to our victims =)
        foreach (var obstacle in victims.Where (ob => ob.CompareTag("Obstacle")))
        {
            obstacle.GetComponentInParent<Obstacle>().SetPlague(true);
        }
        
        AudioManager.Instance.Play("MissilePop");
        
        Destroy(gameObject);
    }

    
    //for showing explosion radius in editor
    private void OnDrawGizmosSelected()
    {
        float scaler = GetComponent<ChargeableObj>().currCharge;
        origin = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, explosionRadius * scaler);
    }
}
