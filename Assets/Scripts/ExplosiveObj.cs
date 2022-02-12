using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveObj : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask layerMask;
    
    private Vector3 origin;
    private Collider[] victims;
    private float radiusScale;
    private void Start()
    {
        radiusScale = GetComponent<Projectile>().radiusesScale;
    }

    public void Explode()
    {
        radiusScale = GetComponent<Projectile>().radiusesScale;
        origin = transform.position;
        victims = Physics.OverlapSphere(origin, explosionRadius*radiusScale, layerMask);
        foreach (var obstacle in victims.Where (ob => ob.CompareTag("Obstacle")))
        {
            obstacle.GetComponentInParent<Obstacle>().SetPlague(true);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        radiusScale = GetComponent<Projectile>().radiusesScale;
        origin = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, explosionRadius * radiusScale);
    }
}
