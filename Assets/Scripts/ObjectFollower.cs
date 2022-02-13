
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform _object;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _object.position, Time.deltaTime*speed);
    }
}
