using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float sphereRadius;
    [SerializeField] protected float maxDistance; 
    [SerializeField] protected LayerMask layerMask;
    
    public float radiusesScale;

    public bool canMove;

    private void Update()
    {
        if (canMove)
        {
            var position = transform.position;
            position = Vector3.MoveTowards(position, position + transform.forward, speed*Time.deltaTime);
            transform.position = position;
        }
    }

    private void FixedUpdate()
    {
        CheckObstacle();
    }
    
    private void CheckObstacle()
    {
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius*radiusesScale, transform.forward, out hit, maxDistance, layerMask))
        {
            GetComponent<ExplosiveObj>()?.Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.forward*maxDistance,sphereRadius*radiusesScale);
    }
}
