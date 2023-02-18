using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class IA_Ship : StatsUnits
{
    private float _time_Move;

    public Rocket _build_Objective;
    protected bool _rocket_InZone = false;

    protected NavMeshAgent _agent;

    protected int _num_ship;
    private bool _eliminate = false;
    protected Manager_IA_Spawn _manager = null;
    
    void Update()
    {
        if(_points_Life <= 0)
        {
            OnDestroy();
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

    void Attack_Rocket()
    {
        if(_build_Objective != null)
        {
            if (_rocket_InZone)
            {
                Shoot(_build_Objective.transform.position);
            }
        }
    }

    protected virtual void Shoot(Vector3 Objective)
    {
        
    }

    void Counter_Attack()
    {
        if(_unit_Objective != null)
        {
            if (_units_InZone.Contains(_unit_Objective))
            {

                Shoot(_unit_Objective.transform.position);
            }
        }
    }

    protected override void Init()
    {
       _manager = FindObjectOfType<Manager_IA_Spawn>();
        _build_Objective = FindObjectOfType<Rocket>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _points_Life_Max = _points_Life;
        Initz();
        On_Move();
    }

    protected virtual void Initz()
    {

    }

    private void On_Move()
    {
        if (_build_Objective != null && _unit_Objective == null)
        {
            this._agent.isStopped = false;
            this._agent.Resume();
            _agent.SetDestination(_build_Objective.transform.position);
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
            _manager.Eliminate_Ship(_num_ship); //Le resto 1 a la casilla 2 del array que es la que contiene las naves de laser.
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
            }
        }


        if (other.gameObject.CompareTag("Bullets"))
        {
            if (other.gameObject.GetComponent<Bullets>()._ship_Shooter.CompareTag("Player") && _units_InZone.Contains(other.gameObject.GetComponent<Bullets>()._ship_Shooter.GetComponent<StatsUnits>()))
            {
                _unit_Objective = other.gameObject.GetComponent<Bullets>()._ship_Shooter.GetComponent<StatsUnits>();
                On_Stop();
                if (!_units_InZone.Contains(_unit_Objective))
                {
                    On_Stop();
                    print("Entro bala");
                    _units_InZone.Add(_unit_Objective);
                }
            }
        }

        if (other.gameObject.CompareTag("Rocket"))
        {
            _rocket_InZone = true;
            On_Stop();
        }
       
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());
            if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective)
            {
                _unit_Objective = null;
                print("Salio el jugador");
                if (!_rocket_InZone)
                {
                    On_Move();
                }
            }
        }

        if (other.gameObject.CompareTag("Rocket"))
        {
            //On_Stop();
            _rocket_InZone = false;
        }
    }

}
