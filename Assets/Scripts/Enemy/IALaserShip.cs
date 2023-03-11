using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using UnityEngine;


//Las naves laser enemigas ignoran el cohete de despegue y se enfocan en atacar las naves del jugador.
public class IALaserShip : StatsUnits
{
    public GameObject _right_Shoot = null;  //Posicion derecha de donde sale el laser
    public GameObject _left_Shoot = null;   //Posicion derecha de donde sale el laser
    private Laser[]  _laser;    //Array de Laser

    private ManagerUnit _manager_Units = null;  //Declaracion al ManagerUnit

    private List<Unit> _troops_Near = new List<Unit>(); //Lista de tropas cercanas

    private NavMeshAgent _agent;    //Declaracion al agente para el PathFinding de Unity

    private float _cooldown_Search = 1f; //se utiliza para limitar la busqueda de tropas cercanas cuando no tengo tropas objetivo.

    public bool _is_Player = false;  //esta variable sirve para distinguir si el laser es disparado por una nave del jugador o por una nave enemiga.

    Manager_IA_Spawn _manager = null;   
    private bool _eliminate = false; //Se utiliza esta variable para eliminar las naves del array de la IA.

    

//Funcion que hereda de StatsUnits. Esta se ejecuta al final de la funcion start.
    protected override void Init(){
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _points_Life_Max = _points_Life;
        _manager_Units = FindObjectOfType<ManagerUnit>();
        On_Move();
        _laser = GetComponentsInChildren<Laser>();
       _manager = FindObjectOfType<Manager_IA_Spawn>();
        _laser[0]._is_Player = _is_Player;
        _laser[1]._is_Player = _is_Player;
        _manager.Add_Ships(1);
    }

    private void Update() {
        if(_points_Life > 0){
        On_Move();
        Attack();
        }else{
            _laser[0].SetLaser(false);
            _laser[1].SetLaser(false);
            OnDestroy();
        }
    }

//funcion que sirve para buscar tropas cercanas. 
//Se llama cada 1seg en On_Move cuando no tengo objetivos para atacar.
    private StatsUnits Search_Units(){
        if(_manager_Units._units.Count > 0){
            _troops_Near.Clear();
            float distance;
            float distance_aux = 1000f; //Sobredimensiono la distancia para poder buscar en todo el mapa.
            StatsUnits troop_return = null;
            foreach(Unit u in _manager_Units._units){
                distance = Vector3.Distance(transform.position, u.transform.position);
                if(distance <= 1000f){
                    
                    if(!_troops_Near.Contains(u)){
                        _troops_Near.Add(u);
                        if(distance < distance_aux){
                            distance_aux = distance;
                            troop_return = u.GetComponent<StatsUnits>();
                        }
                    }
            
                }
            }

            return troop_return;
        }else{
            return null;    //Si no hay tropas del jugador disponible retorna nulo.
        }
    }

//Metodo que se ejecuta a la hora de atacar.
    private void Attack(){
        try{
            if (_unit_Objective != null && _units_InZone.Contains(_unit_Objective)){    //Si no tengo una tropa objetivo asignada.
                
                if(_unit_Objective._points_Life <= 0){  //Si elimino a la tropa que tengo como objetivo.
                    _units_InZone.Remove(_unit_Objective);  //La remuevo de las unidades en la zona.
                    _unit_Objective = null; //COnvierto en nulo
                    if(_units_InZone[0] != null){   //Si hay mas unidades por el mundo.
                        _unit_Objective = _units_InZone[0]; //Se la asigno como objetivo.
                    }
                    else
                    {
                        if (_units_InZone[1] != null)   //Si el objetivo en la posicion 0 es nula asigno la de la posicion 1.
                        {
                            _unit_Objective = _units_InZone[1];
                        }
                        else
                        {   //Sino, deshabilito los laseres
                            _laser[0].SetLaser(false);  
                            _laser[1].SetLaser(false);
                        }
                    }
                    _laser[0].SetLaser(false);
                    _laser[1].SetLaser(false);
                }
                else{   //En cambio si tengo una tropa ya asignada.

                    foreach (var unit in _units_InZone)
                    {   
                        //Habilito los laseres
                        _laser[0].SetLaser(true);   
                        _laser[1].SetLaser(true);
                    
                        _cooldown_Current -= Time.deltaTime;

                        if (_cooldown_Current <= 0){//Si el tiempo es 0, ataco.
                            _laser[0].Attack = true;
                            _laser[1].Attack = true;
                            _cooldown_Current = _cooldown_Attack;   //Reinicio el tiempo.
                            
                        }
                        this.transform.LookAt(_unit_Objective.transform.position);  //Giro la nave para que mire hacia el objetivo.
                    }
                }
            }
            else
            {
                _laser[0].SetLaser(false);
                _laser[1].SetLaser(false);
            }
        }catch{

        }
    }
    

//Metodo que se utiliza para mover la nave.
    private void On_Move()
    {
        if (_unit_Objective != null)    //Si tengo asignado un objetivo, muevo la nave.
        {
            if(!_units_InZone.Contains(_unit_Objective)){
                this._agent.isStopped = false;
                this._agent.Resume();
                _agent.SetDestination(_unit_Objective.transform.position);  //Le asigno el destino a la nave.
            }else{
                this.On_Stop();
            }
        }else{  //Sino, realizo la busqueda de nuevas naves.
            _cooldown_Search -= Time.deltaTime;
            if(_cooldown_Search <= 0){
                _unit_Objective = Search_Units();
                if(_unit_Objective == null){    //Si no tengo naves objetivos
                    this._ship.OnStop();    //Detengo la nave.
                }
                _cooldown_Search = 1f;  //Reinicio el tiempo de busqueda.
            }
        }
    }
    

    private void On_Stop()
    {
        this._agent.isStopped = true;
    }

    private void OnDestroy()
    {
        Delete_Ship_List();
        GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject, 5f);
    }

    private void Delete_Ship_List(){
        if(!_eliminate){
            _eliminate = true;
            _manager.Eliminate_Ship(1); //Le resto 1 a la casilla 2 del array que es la que contiene las naves de laser.
        }
    }


     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_units_InZone.Contains(other.gameObject.GetComponent<StatsUnits>()))
            {
                _units_InZone.Add(other.gameObject.GetComponent<StatsUnits>());
                _unit_Objective = other.GetComponent<StatsUnits>();
                _ship.OnStop();
            }
            _laser[0]._objective = _unit_Objective.gameObject;
            _laser[1]._objective = _unit_Objective.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());
            if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective)
            {
                if(_units_InZone.Count >=1){
                    _unit_Objective = _units_InZone[0];
                    print("Salio el jugador");
                }else{
                    _unit_Objective = null;
                }
            }
        }
    }

}