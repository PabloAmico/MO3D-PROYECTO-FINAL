using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectShip : StatsUnits
{
    public bool _is_Big;    //Tamaño de la roca que esta recolectando. True = grande, False = chica.
    public bool _contact = false;   //Si hizo contacto con la roca.

    public int _collect_Money = 0;  //Atributo que sirve para cuantas rocas puede recolectar por vuelta.
 
    private Money _money_Text;  //Texto del dinero.

    public float _timeRecollect;    //Tiempo de recoleccion
    private float _timePaid;    //Tiempo de pago
    public RockOfMoney _objective_Recollect = null; //Objetivo al cual recolectar.

    public bool _destiny = false;   //Es True si tiene un destino al cual ir.

    public PoolRockOfMoney _pool_Money; //Pool de las rocas que contienen el dinero.

    //GUI
    public Image _circle_Fill;  //Circulo que muestra el porcentaje de recoleccion

    public Image _icon; //Icono de recoleccion.

    public bool _sync = false;  //Si se encuentra sincronizado con la clase Money.
   

   //Clase heredada de StatsUnits
     protected override void Init()
    {
        _money_Text = FindObjectOfType<Money>();    //Le asigno el objeto Money que se encuentra en la escena.
        
        _pool_Money = FindObjectOfType<PoolRockOfMoney>();  //Le asigno el objeto PoolRockOfMoney que se encuentra en la escena.
      
        _timePaid = 0; 

        //Inabhilito las imagenes del GUI
        _circle_Fill.enabled = false;
        _icon.enabled = false;

    }

    void Update()
    {
        //Si no tengo un destino
        if(!_destiny){
            Set_Destiny();
        }else{  //Si lo tengo e hice contacto con el objetivo pero no estoy sincronizado.
            if (_contact && !_sync){
                
                _sync = _money_Text.GoSync();   //Pido sincronizarme con el reloj de pago.
                _ship.OnStop(); //Detengo la nave.
            }
        }
        Move_Ship();    //Muevo la nave.
        if (_contact && _sync)  //Si hice contacto y estoy sincronizado.
        {
            //Habilito las imagenes del GUI
            _circle_Fill.enabled = true;
            _icon.enabled = true;

            _ship.OnStop(); //Detengo la nave
            //VOy llenando el circulo de carga
            _circle_Fill.fillAmount = _money_Text.Get_Current_Time_Paid() / _money_Text.Get_Time_Paid();
            //Si el tiempo actual es mayor al de pago
            if(_money_Text.Get_Current_Time_Paid() >= _money_Text.Get_Time_Paid())
            {
                Collect_Money(_collect_Money);  //Recolecto el dinero.
            }
        }else{  //Sino, desactivo las imagenes GUI.
            _circle_Fill.enabled = false;
            _icon.enabled = false;
        }
    }

    
//Metodo para recolectar el dinero.
    public void Collect_Money(int Number)
    {
        if(_objective_Recollect != null){   //Si tengo un objetivo para recolectar.
            if (_objective_Recollect.Get_Current_Money() >= Number) //Y tengo disponible para recolectar mas de lo que recolecta la nave
            {
                
                _money_Text.Add_New_Money(Number);  //Recolecto el total de capacidad de la nave
                _objective_Recollect.GetComponent<RockOfMoney>().Extract_Money(Number); //Quito ese numero a la roca de dinero.
            }
            else
            {//sino extraigo lo que quede en la roca de dinero
                _money_Text.Add_New_Money(_objective_Recollect.Get_Current_Money());    
                _objective_Recollect.GetComponent<RockOfMoney>().Extract_Money(_objective_Recollect.Get_Current_Money());
            }
            if(_objective_Recollect.Get_Current_Money() < 0)    //Si la roca no tiene mas dinero
            {
                _contact = false;   //contacto es falso
                _objective_Recollect = null;    //No tengo objetivo de recoleccion
                _destiny = false;   //No tengo destino.
            }
        }
    }

   
//Metodo para setear el objetivo a recolectar.
    public void Set_Recollect(RockOfMoney recollect)
    {
        _objective_Recollect= recollect;
        if(recollect == null){  //Si es nulo
            _destiny = false;   //no tengo destino.
        }
    }

//Metodo para elegir el proximo destino a recolectar.
    public void Set_Destiny(){
        if(!_destiny){  //Si no tengo destino al cual ir
            float distance = 10000f;    //Aplico una distancia exagerada.
           //aplicar contains.
            foreach (RockOfMoney rock in _pool_Money.Get_Rocks())   //Recorro mi array de rocas.
            {
                float aux_distance = Vector2.Distance(this.transform.position, rock.transform.position);    //Obtengo la distancia entre la roca y mi nave.
                if(aux_distance < distance){    //corroboro la distancia con la distancia que tengo guardada
                    _objective_Recollect = rock;    //Le asigno el objetivo.
                    distance = aux_distance;
                }
            }
            if(_objective_Recollect != null){   //si tengo un destino
                _destiny = true;    //Tengo un destino
            }
        }
    }

//Metodo para mover la nave.
    private void Move_Ship(){
        if( _destiny && !_contact){ //Si tengo un destino y no estoy en contacto con la roca
            _ship.OnMove(_objective_Recollect.transform.position);  //muevo la nave
        }
    }

  private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("RockOfMoney")){
            _contact = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("RockOfMoney")){
            _contact = false;
        }
    }
}

  