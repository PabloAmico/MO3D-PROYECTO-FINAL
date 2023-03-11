using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//Esta clase hereda de StatsUnits. Se encarga en controlar el comportamiento de la nave, como su movimiento y ataque.
public class IA_Ship : StatsUnits
{
    //private float _time_Move;

    public Rocket _build_Objective; //Utilizo el cohete para saber su ubicacion y desplazarme hacia el.
    protected bool _rocket_InZone = false;  //variable que se utiliza para saber si el cohete se encuentra en la zona, para luego atacarlo.

    protected NavMeshAgent _agent;  //el agente de navegacion para utilizar el pathfinding que incluye Unity.

    protected int _num_ship;    //variable que se utiliza para eliminar la nave y controlar la cantidad de instancias.
    private bool _eliminate = false;    //si la nave se elimina se vuelve true.
    protected Manager_IA_Spawn _manager = null; //El manager de spawn de naves.

    private bool _destroy = false;  //variable que se utiliza para ejecutar una sola vez la destruccion de la nave.
    
    void Update()
    {
        if(_points_Life <= 0 && !_destroy) 
        {
            OnDestroy();
            _destroy = true;
        }
        if(_unit_Objective != null)
        {
            Counter_Attack();
        }
        else
        {
            Attack_Rocket();
        }
        

    }

//Funcion que se ejecuta si la nave tiene a su alcance el cohete.
    void Attack_Rocket()
    {
        if(_points_Life > 0){   //Si esta nave esta viva.
            if(_build_Objective != null)    //si existe el cohete.
            {
                if (_rocket_InZone) //si el cohete se encuentra en la zona de ataque
                {
                    Shoot(_build_Objective.transform.position); //se ataca
                }else{
                    On_Move();  //sino se mueve hacia ella.
                }
            }
        }
    }

//cada clase que hereda de IA_ship ejecuta a su forma esta funcion. Por eso es virtual.
    protected virtual void Shoot(Vector3 Objective) 
    {
        
    }

//Si atacan a la nave esta responde.
    void Counter_Attack()
    {
        if(_points_Life > 0){   //Si esta nave esta viva.
            if(_unit_Objective != null) //Si la nave que la ataco existe
            {
                if (_units_InZone.Contains(_unit_Objective))    //y se encuentra en la zona.
                {

                    Shoot(_unit_Objective.transform.position);  //Le disparo.
                }
            }
        }
    }

//Funcion que hereda de StatsUnits. Esta se ejecuta al final de la funcion start.
    protected override void Init()
    {
       _manager = FindObjectOfType<Manager_IA_Spawn>();
        _build_Objective = FindObjectOfType<Rocket>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _points_Life_Max = _points_Life;
        Initz();    
        On_Move();
    }

/*Esta funcion se ejecuta dentro de la funcion Init.
Se crea porque cada tipo de nave que hereda de esta clase necesita instanciar o prepararse de distinta manera.
Por eso es Virtual.
*/
    protected virtual void Initz()
    {

    }

//funcion de la cual depende el movimiento.
    private void On_Move()
    {
        if (_build_Objective != null && _unit_Objective == null)    //Si no tiene nave del jugador para atacar se mueve hacia el objetivo.
        {
            this._agent.isStopped = false;
            this._agent.Resume();
            _agent.SetDestination(_build_Objective.transform.position);
        }else{
            if(_unit_Objective != null || _destroy){    //Si tiene una nave del jugador como objetivo
                On_Stop();  //Se detiene.
            }
        }
    }


/*Funcion que se utiliza para detener la nave.
Utiliza la funcion Stop de los agentes de pathfinding que incluye unity
*/
    private void On_Stop()
    {
        try{
            this._agent.isStopped = true;
        }catch{}
    }


//Funcion que se llama al destruir la nave.
    private void OnDestroy()
    {
        //On_Stop();
        Delete_Ship_List();
        GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject, 5f);
    }


//Metodo que elimina la nave del array de Manager_IA_Spawn.
     private void Delete_Ship_List(){
        if(!_eliminate){
            _eliminate = true;
            _manager.Eliminate_Ship(_num_ship); //Le resto 1 a la casilla 2 del array que es la que contiene las naves de laser.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        try{
            if(other != null){
                if (other.gameObject.CompareTag("Player"))  //Si ingresa una nave del jugador a la zona 
                {
                    if (!_units_InZone.Contains(other.gameObject.GetComponent<StatsUnits>()))   
                    {
                        _units_InZone.Add(other.gameObject.GetComponent<StatsUnits>()); //Lo agrego a las naves dentro de la zona
                    }
                }
                else{

                    if (other.gameObject.CompareTag("Bullets")) //Si estan atacando a la nave
                    {
                        if (other.gameObject.GetComponent<Bullets>()._ship_Shooter.CompareTag("Player") && _units_InZone.Contains(other.gameObject.GetComponent<Bullets>()._ship_Shooter.GetComponent<StatsUnits>()))
                        {
                            _unit_Objective = other.gameObject.GetComponent<Bullets>()._ship_Shooter.GetComponent<StatsUnits>();    //agrego la nave que ataco como objetivo
                            On_Stop();  //detengo la nave
                            if (!_units_InZone.Contains(_unit_Objective))
                            {
                                On_Stop();
                               
                                _units_InZone.Add(_unit_Objective);
                            }
                        }
                        }else{

                            if (other.gameObject.CompareTag("Rocket"))  //Si el cohete esta en el rango de ataque, la nave se detiene.
                            {
                                _rocket_InZone = true;
                                On_Stop();
                            }
                        }
                    }

                
                }
            }catch{
            
        }
       
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //si el jugador sale de la zona de ataque 
        {
           
            _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());  //Lo remuevo de las tropas que se encuentran en la zona.
            if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective)
            {
                _unit_Objective = null;
                if (!_rocket_InZone)
                {
                    On_Move();
                }
            }
        }

        if (other.gameObject.CompareTag("Rocket"))
        {
            _rocket_InZone = false;
        }
    }

}
