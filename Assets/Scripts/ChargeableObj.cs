using UnityEngine;

public class ChargeableObj : MonoBehaviour
{
    //current charge is [minCharge] after creating object
    [SerializeField] private float minCharge;
    //current charge can`t be higher then [maxCharge]
    [SerializeField] private float maxCharge;
    [SerializeField] private Transform objectToCharge;
    
    
    public float currCharge { get; private set; }
    private void Start()
    {
        currCharge = minCharge;
        //set scale according to charge
        objectToCharge.localScale = Vector3.one * currCharge;
    }

    public void AddCharge(float charge)
    {
        if (objectToCharge == null) return;
        currCharge += charge;
        currCharge = Mathf.Min(maxCharge, Mathf.Max(minCharge, currCharge));
        objectToCharge.localScale = Vector3.one * currCharge;
    }
    
}
