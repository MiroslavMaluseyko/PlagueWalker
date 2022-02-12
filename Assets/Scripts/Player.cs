using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

enum PlayerState
{
    Charging,
    Moving,
    WaitingForShot,
    WaitingForMove
}

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerSphere;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private ChargeableObj missilePrefab;
    [SerializeField] private float distToObstacle;
    [SerializeField] private LayerMask layerMask;
    public float charge;

    private PlayerState currState;

    private ChargeableObj missile;
    private void Start()
    {
        currState = PlayerState.WaitingForShot;
        playerSphere.transform.localScale = Vector3.one * charge;
        transform.LookAt(GameManager.Instance.target.transform.position);
        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currState == PlayerState.WaitingForShot && missile == null)
        {
            currState = PlayerState.Charging;
            CreateMissile();
        }
        else if (Input.GetMouseButton(0) && currState == PlayerState.Charging)
        {
            RemoveCharge(GameManager.Instance.chargePerFrame);
            missile.AddCharge(GameManager.Instance.chargePerFrame);
        }
        else if (Input.GetMouseButtonUp(0) && currState == PlayerState.Charging)
        {
            currState = PlayerState.WaitingForMove;
            missile.GetComponent<Projectile>().canMove = true;
        }

        if (currState == PlayerState.WaitingForMove && missile == null)
        {
            currState = PlayerState.Moving;
            StartCoroutine(MovingCoroutine());
        }
    }

    private void CreateMissile()
    {
        missile = Instantiate(missilePrefab, gunpoint.position, gunpoint.rotation);
    }
    
    
    public void RemoveCharge(float val)
    {
        charge -= val;
        playerSphere.transform.localScale = Vector3.one * charge;

        if (charge <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator MovingCoroutine()
    {
        RaycastHit hit;
        while (!Physics.SphereCast(transform.position, charge/2, transform.forward, out hit, distToObstacle,
                   layerMask))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed*Time.fixedDeltaTime);
            yield return null;
        }

        currState = PlayerState.WaitingForShot;
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.forward*distToObstacle,charge/2);
    }
}
