using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerSphere;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private ChargeableObj missilePrefab;
    public float charge;

    private ChargeableObj missile;
    private void Start()
    {
        playerSphere.transform.localScale = Vector3.one * charge;
        transform.LookAt(GameManager.Instance.target.transform.position);
        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateMissile();
        }
        else if (Input.GetMouseButton(0))
        {
            RemoveCharge(GameManager.Instance.chargePerFrame);
            missile.AddCharge(GameManager.Instance.chargePerFrame);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            missile.GetComponent<Projectile>().canMove = true;
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
}
