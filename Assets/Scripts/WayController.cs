
using UnityEngine;

public class WayController : MonoBehaviour
{
    //renderer that should draw line
    private LineRenderer _lineRenderer;
    //transform of player
    private Transform playerTransform;
    //player script for tracking charge
    private Player player;
    //transform of target
    private Transform target;
    
    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        player = FindObjectOfType<Player>();
        target = FindObjectOfType<Target>().transform;
        
        _lineRenderer = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        //set one side under the player
        Vector3 pos = playerTransform.position;
        pos.y = 0.01f;
        _lineRenderer.SetPosition(0, pos);
        //set other side under the target
        pos = target.position;
        pos.y = 0.01f;
        _lineRenderer.SetPosition(1, pos);
        //set width according to charge
        _lineRenderer.widthCurve = AnimationCurve.Constant(0, 0, player.charge);
    
    }
}
