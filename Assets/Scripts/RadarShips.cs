using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarShips : MonoBehaviour
{
    private SFX_Radio _radio;
    // Start is called before the first frame update
    void Start()
    {
        _radio = FindObjectOfType<SFX_Radio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ship Enemy"))
        {
            //print("Entrooooo " + other.gameObject.name);
            _radio.Enemy_Spotted();
            GetComponentInParent<StatsUnits>()._ship.OnStop();
            GetComponentInParent<StatsUnits>()._units_InZone.Add(other.gameObject.GetComponent<StatsUnits>());
        }
    }
}
