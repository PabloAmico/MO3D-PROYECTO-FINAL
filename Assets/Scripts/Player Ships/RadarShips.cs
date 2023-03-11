using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarShips : MonoBehaviour
{
    private SFX_Radio _radio;   //Obtengo la radio de la escena
    // Start is called before the first frame update
    void Start()
    {
        _radio = FindObjectOfType<SFX_Radio>();
    }

    private void OnTriggerEnter(Collider other)
    {   //Si una nave enemiga entra a la zona de ataque de esta nave se agrega a las unidades que se encuentran en la zona.
        if(other.gameObject.CompareTag("Ship Enemy")) 
        {
            _radio.Enemy_Spotted();
            GetComponentInParent<StatsUnits>()._ship.OnStop();
            GetComponentInParent<StatsUnits>()._units_InZone.Add(other.gameObject.GetComponent<StatsUnits>());
        }
    }
}
