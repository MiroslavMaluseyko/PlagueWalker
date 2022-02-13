using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //moving speed
    [SerializeField] protected float speed;
    //object will be destroyed after [lifeTimeSec] seconds
    [SerializeField] protected float lifeTimeSec;
    //radius of sphereCast`s sphere
    [SerializeField] protected float sphereRadius;
    //max distance for sphereCast
    [SerializeField] protected float maxDistance; 
    //obstacle layer for spherecast
    [SerializeField] protected LayerMask layerMask;


    private ChargeableObj chargeScript;
    private bool canMove;

    private void Start()
    {
        chargeScript = GetComponent<ChargeableObj>();
    }
    
    //allow object to move and play all needed effects
    public void Shoot()
    {
        StartCoroutine(ProjectileDestruction());
        canMove = true;
    }
    
    private void FixedUpdate()
    {
        if (canMove)
        {
            var position = transform.position;
            position = Vector3.MoveTowards(position, position + transform.forward, speed*Time.deltaTime);
            transform.position = position;
        }
        //checking for collision with obstacle 
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius*chargeScript.currCharge, transform.forward, out hit, maxDistance, layerMask))
        {
            GetComponent<ExplosiveObj>()?.Explode();
        }
    }

    private IEnumerator ProjectileDestruction()
    {
        yield return new WaitForSeconds(lifeTimeSec);
        Destroy(gameObject);
    }
    

}
