using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShipAttack : StatsUnits
{
    public GameObject _right_Shoot = null;
    public GameObject _left_Shoot = null;
    private PoolBullets _pool;
    Bullets _bullets_Right;
    Bullets _bullets_Left;
    private Rocket _rocket;

    // Update is called once per frame
    void Update()
    {
        Attack();
        if(_points_Life <= 0)
        {
            _show_Select.Set_Show(false);
            OnDestroy();
            //Kill_Unit();
        }

        
        
    }

    private void OnDestroy()
    {
        //Remover del manager unit
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        Destroy(this.gameObject, 5f);
    }
    protected override void Init()
    {
        _points_Life_Max = _points_Life;
        _rocket = FindObjectOfType<Rocket>();
        //_particleSystem.Stop();
        _pool = FindObjectOfType<PoolBullets>();
    }

   

    private void Attack()
    {
        
        _cooldown_Current -= Time.deltaTime;
        if (_unit_Objective != null)
        {
            foreach (var unit in _units_InZone)
            {

                if (_unit_Objective.GetComponent<StatsUnits>()._points_Life > 0)
                {
                    if (_cooldown_Current <= 0)
                    {
                        _bullets_Right = _pool.Assign_Bullet();
                        _bullets_Right.Assign_PosAndRot(_right_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);
                        _bullets_Right.Set_Damage(_points_Attack);
                        _bullets_Right.Assign_Shooter(this.gameObject);


                        _bullets_Left = _pool.Assign_Bullet();
                        _bullets_Left.Assign_PosAndRot(_left_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);
                        _bullets_Left.Set_Damage(_points_Attack);
                        _bullets_Left.Assign_Shooter(this.gameObject);


                        _cooldown_Current = _cooldown_Attack;
                        this.transform.LookAt(_unit_Objective.transform.position);
                    }
                }
            }
        }
        else
        {
            if (_rocket._enemy_List.Count > 0 && _zone_Rocket)
            {
                _unit_Objective = _rocket._enemy_List[0].GetComponent<StatsUnits>();
                if (!_units_InZone.Contains(_unit_Objective) && _unit_Objective.GetComponent<IABasicShip>()._points_Life > 0)
                {
                    _ship.OnMove(_unit_Objective.transform.position);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {
            _unit_Objective = other.gameObject.GetComponent<StatsUnits>();
            _units_InZone.Add(other.gameObject.GetComponent<StatsUnits>());
            if (_unit_Objective != null)
            {
                if (_unit_Objective == other.gameObject.GetComponent<StatsUnits>() && _ship._select_Attack)
                {
                    _ship.OnStop();
                }
            }
        }

        if (other.gameObject.CompareTag("Zone Rocket"))
        {
            _zone_Rocket = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {
            if (other.gameObject != null)
            {
               
                _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());
                if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective)
                {
                    _unit_Objective = null;
                }
            }

        }

        if (other.gameObject.CompareTag("Zone Rocket"))
        {
            _zone_Rocket = false;
        }
    }
}
