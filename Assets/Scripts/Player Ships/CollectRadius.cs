using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase utilizada por la nave de recoleccion.
public class CollectRadius : MonoBehaviour
{
    private RecollectShip _ship;
    void Start()
    {
        _ship = GetComponentInParent<RecollectShip>();
      
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RockOfMoney")  //Si tiene cerca una roca de dinero.
        {
            _ship.Set_Recollect(other.gameObject.GetComponent<RockOfMoney>());  //La recolecta
            GetComponentInParent<StatsUnits>()._ship.OnStop();  //Se detiene.
            other.GetComponent<RockOfMoney>().Set_Ship(_ship);
            _ship._contact = true;  
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "RockOfMoney") //Si esa roca se elimina 
        {
            _ship.Set_Recollect(null);  //Se setea la recoleccion
            _ship._sync = false;
            _ship._contact = false;
        }
    }

  
}
