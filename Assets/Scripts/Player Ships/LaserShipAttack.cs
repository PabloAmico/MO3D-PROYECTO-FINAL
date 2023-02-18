using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShipAttack : StatsUnits
{

    public GameObject _right_Shoot = null;
    public GameObject _left_Shoot = null;
    private Rocket _rocket;
    private Laser[]  _laser;

    public bool _is_Player = true;
    // Start is called before the first frame update
    

    protected override void Init()
    {
        
        _points_Life_Max = _points_Life;
        _rocket = FindObjectOfType<Rocket>();
        _laser = GetComponentsInChildren<Laser>();
        _laser[0]._is_Player = _is_Player;
        _laser[1]._is_Player = _is_Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (_points_Life <= 0)
        {
            _show_Select.Set_Show(false);
            _laser[0].SetLaser(false);
            _laser[1].SetLaser(false);
            OnDestroy();
            
        }
        else
        {
            Attack();
        }
    }

    private void OnDestroy()
    {
        //Remover del manager unit
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        Destroy(this.gameObject, 5f);
    }

    private void Attack()
    {
        if(_unit_Objective != null)
        {
            if (_unit_Objective._points_Life <= 0)
            {
                _unit_Objective = null;
                if(_units_InZone.Count > 0){
                    if (_units_InZone[0] != null)
                    {
                        if (_units_InZone[0]._points_Life > 0)
                        {
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
                }
            }
            else
            {
                foreach (var unit in _units_InZone)
                {

                    _laser[0].SetLaser(true);
                    _laser[1].SetLaser(true);
                    _cooldown_Current -= Time.deltaTime;
                    if (_cooldown_Current <= 0)
                    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {
            _unit_Objective = other.gameObject.GetComponent<StatsUnits>();
            _laser[0]._objective = _unit_Objective.gameObject;
            _laser[1]._objective = _unit_Objective.gameObject;
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
