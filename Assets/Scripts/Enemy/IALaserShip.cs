//Las naves laser enemigas ignoran el cohete de despegue y se enfocan en atacar las naves del jugador.

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using UnityEngine;

public class IALaserShip : StatsUnits
{
    public GameObject _right_Shoot = null;
    public GameObject _left_Shoot = null;
    private Laser[]  _laser;

    private ManagerUnit _manager_Units = null;

    private List<Unit> _troops_Near = new List<Unit>();

    private NavMeshAgent _agent;

    private float _cooldown_Search = 1f; //se utiliza para limitar la busqueda de tropas cercanas cuando no tengo tropas objetivo.

    public bool _is_Player = false;  

    Manager_IA_Spawn _manager = null;
    private bool _eliminate = false; //Se utiliza esta variable para eliminar las naves del array de la IA.

    

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
            float distance_aux = 1000f;
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
            return null;
        }
    }

    private void Attack(){
        if (_unit_Objective != null && _units_InZone.Contains(_unit_Objective)){
            
            if(_unit_Objective._points_Life <= 0){
                print("Objetivo muerto");
                _units_InZone.Remove(_unit_Objective);
                _unit_Objective = null;
                if(_units_InZone[0] != null){
                    _unit_Objective = _units_InZone[0];
                }
                else
                {
                    if (_units_InZone[1] != null)
                    {
                        _unit_Objective = _units_InZone[1];
                    }
                    else
                    {
                        _laser[0].SetLaser(false);                           
                        _laser[1].SetLaser(false);
                    }
                }
                _laser[0].SetLaser(false);
                _laser[1].SetLaser(false);
            }
            else{
                    //print("ATACAR!");
                foreach (var unit in _units_InZone)
                {   
                    _laser[0].SetLaser(true);
                    _laser[1].SetLaser(true);
                   
                    _cooldown_Current -= Time.deltaTime;
                    //print("cooldown: " + _cooldown_Current);
                    if (_cooldown_Current <= 0){
                        _laser[0].Attack = true;
                        _laser[1].Attack = true;
                        _cooldown_Current = _cooldown_Attack;
                        
                    }
                    this.transform.LookAt(_unit_Objective.transform.position);
                }
            }
        }
        else
        {
            _laser[0].SetLaser(false);
            _laser[1].SetLaser(false);
        }
    }
    

    private void On_Move()
    {
        if (_unit_Objective != null)
        {
            if(!_units_InZone.Contains(_unit_Objective)){
                this._agent.isStopped = false;
                this._agent.Resume();
                _agent.SetDestination(_unit_Objective.transform.position);
            }else{
                this.On_Stop();
            }
        }else{
            _cooldown_Search -= Time.deltaTime;
            if(_cooldown_Search <= 0){
                _unit_Objective = Search_Units();
                if(_unit_Objective == null){
                    this._ship.OnStop();
                }
                _cooldown_Search = 1f;
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
                //print("Entro player");
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
            //print("Salio el jugador");
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