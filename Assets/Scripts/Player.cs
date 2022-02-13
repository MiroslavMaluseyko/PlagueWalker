using UnityEngine;

public class Player : MonoBehaviour
{
    //moving speed
    [SerializeField] private float speed;
    //Value when game lost
    [SerializeField] private float minCharge;
    //player`s 3d object
    [SerializeField] private GameObject playerSphere;
    //point from where player shooting
    [SerializeField] private Transform gunpoint;
    //prefab of missile to shoot
    [SerializeField] private ChargeableObj missilePrefab;
    //distance at which the player will stop
    [SerializeField] private float distToObstacle;
    //obstacle mask for rayCasting
    [SerializeField] private LayerMask layerMask;
    
    //current charge value
    public float charge;

    private Rigidbody rb;
    //chargeable script of missile for.. missile charging
    private ChargeableObj missile;
    private bool isCharging;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //set player`s size according to charge
        playerSphere.transform.localScale = Vector3.one * charge;
        
        //turn player to face the target
        Vector3 target = FindObjectOfType<Target>().transform.position;
        Vector3 lookDir = target;
        lookDir.y = transform.position.y;
        transform.LookAt(lookDir);

    }

    private void Update()
    {
        if (!GameManager.Instance.gamePaused)
        {
            bool touching = Input.touches.Length > 0;

            if ((Input.GetMouseButtonDown(0) || touching) && !isCharging)
            {
                //checking for UI zone. We do not want shot when clicking on pause
                if (Camera.main.ScreenToViewportPoint(Input.mousePosition).y > 0.9f) return;
                CreateMissile();
                isCharging = true;
            }
            else if ((Input.GetMouseButton(0) || touching) && isCharging)
            {
                RemoveCharge(GameManager.Instance.chargePerFrame);
                missile.AddCharge(GameManager.Instance.chargePerFrame);
            }
            else if ((Input.GetMouseButtonUp(0) || !touching) && isCharging)
            {
                isCharging = false;
                missile.GetComponent<Projectile>().Shoot();
            }
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

        if (charge <= minCharge)
        {
            isCharging = false;
            GameManager.Instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        //player can`t moving during charging
        if (!isCharging)
        {
            RaycastHit hit;
            if (!Physics.SphereCast(transform.position, charge / 2, transform.forward, out hit, distToObstacle,
                    layerMask))
            {
                rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.GetComponent<Door>()?.Open();
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.GameWin();
        }
    }


    //for testing sphereCast
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.forward*distToObstacle,charge/2);
    }
}
