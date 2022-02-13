
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Start()
    {
        //turn target to face the player
        Vector3 lookDir = FindObjectOfType<Player>().transform.position;
        lookDir.y = transform.position.y;
        transform.LookAt(lookDir);
    }
}
