using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRadius : MonoBehaviour
{
    private RecollectShip _ship;
   //private LaserRecollect _laser;
    // Start is called before the first frame update
    void Start()
    {
        _ship = GetComponentInParent<RecollectShip>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RockOfMoney")
        {
            ///print("Shit shit money money");
            _ship.Set_Recollect(other.gameObject.GetComponent<RockOfMoney>());
            GetComponentInParent<StatsUnits>()._ship.OnStop();
            other.GetComponent<RockOfMoney>().Set_Ship(_ship);
           
            _ship._contact = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        //print("Algo salio");
        if(other.tag == "RockOfMoney")
        {
            print("ME FUII");
            _ship.Set_Recollect(null);
            _ship._sync = false;
            _ship._contact = false;
        }
    }

  
}
