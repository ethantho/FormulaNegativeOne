using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePad : MonoBehaviour
{
    public bool charging = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        charging = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnergyBar nrg = collision.gameObject.GetComponentInChildren<EnergyBar>();
        //Debug.Log(nrg);
        
        if (nrg != null)
        {
            nrg.replenishEnergy(20 * Time.deltaTime);
            charging = true;
        }
    }
}
