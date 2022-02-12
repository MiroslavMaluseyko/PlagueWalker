using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool plagued;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color plagueColor;


    private void Start()
    {
        SetPlague(plagued);
    }

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
        Collider coll = GetComponentInChildren<Collider>();
        coll.enabled = false;
        yield return null;
    }
}
