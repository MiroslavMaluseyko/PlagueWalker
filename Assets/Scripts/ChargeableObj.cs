using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChargeableObj : MonoBehaviour
{
    [SerializeField] private float minCharge;
    [SerializeField] private float maxCharge;
    [SerializeField] private float currCharge;
    [SerializeField] private Transform objectToCharge;

    private Projectile projectile;
    private void Start()
    {
        currCharge = minCharge;
        objectToCharge.localScale = Vector3.one * currCharge;
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
    }

    public void AddCharge(float charge)
    {
        currCharge += charge;
        currCharge = Mathf.Min(maxCharge, Mathf.Max(minCharge, currCharge));
        objectToCharge.localScale = Vector3.one * currCharge;
        projectile.radiusesScale = 1 * currCharge;
    }
    
}
