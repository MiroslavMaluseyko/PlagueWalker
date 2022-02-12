using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    private void Start()
    {
        
        _lineRenderer = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        Vector3 pos = GameManager.Instance.player.transform.position;
        pos.y = 0.01f;
        _lineRenderer.SetPosition(0, pos);
        pos = GameManager.Instance.target.transform.position;
        pos.y = 0.01f;
        _lineRenderer.SetPosition(1,pos);
        _lineRenderer.widthCurve = AnimationCurve.Constant(0,0,GameManager.Instance.player.charge);
    }
}
