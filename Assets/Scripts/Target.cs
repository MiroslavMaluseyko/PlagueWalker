using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Start()
    {
        transform.LookAt(GameManager.Instance.player.transform.position);
        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        transform.rotation = rotation;
    }
}
