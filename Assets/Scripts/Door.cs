using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    //object to animate
    [SerializeField] private Transform doorObj;
    //position in world when open
    [SerializeField] private Vector3 openPos;
    //position in world when closed
    [SerializeField] private Vector3 closePos;

    [SerializeField] private bool isOpen;
    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(OpeningCoroutine());
        }
    }
    
    private IEnumerator OpeningCoroutine()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            doorObj.localPosition = Vector3.Lerp(closePos, openPos, i);
            yield return null;
        }
    }
}
