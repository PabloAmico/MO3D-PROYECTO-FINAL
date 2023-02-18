using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShipAttack : StatsUnits
{
    public GameObject _missile_Shoot = null;
    private PoolMissile _pool;
    private Missile _missile;
    private float _zone_Damage; //tama�o de la esfera de explosion.
    private Rocket _rocket;
    //private List<StatsUnits> _enemyZoneExplosion = new List<StatsUnits>();


    protected override void Init()
    {
        _points_Life_Max = _points_Life;

        _rocket = FindObjectOfType<Rocket>();
        _pool = FindObjectOfType<PoolMissile>();
        //Debug.Log("Tipo de misil" + _pool);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if(_points_Life <= 0)
        {
            _show_Select.Set_Show(false);
            OnDestroy();
        }
    }

    public void Set_Zone_Damage(float Zone)
    {
        _zone_Damage = Zone;
    }

    public void Zone_Explosion(List<StatsUnits> Enemies)
    {
        print("ZONE EXPLOSION " + Enemies.Count);
        foreach(StatsUnits unit in Enemies)
        {
            print("Unit " + unit.gameObject.name);
            Execute_Explosion_Damage(unit);
        }
    }

    private void Execute_Explosion_Damage(StatsUnits Enemy)
    {
        /*float Distance = Vector3.Distance(this.gameObject.transform.position, Enemy.transform.position);
        if(Distance < _zone_Damage / 3)
        {
            Enemy.GetComponent<StatsUnits>()._points_Life = _points_Attack / 2;
            print("ENEMY LIFE 1 " + Enemy.GetComponent<StatsUnits>()._points_Life);
        }
        else
        {
            if(Distance >= _zone_Damage / 3 && Distance <= _zone_Damage / 2)
            {
                Enemy.GetComponent<StatsUnits>()._points_Life = _points_Attack / 3;
                print("ENEMY LIFE 2 " + Enemy.GetComponent<StatsUnits>()._points_Life);
            }
            else
            {
                if(Distance > _zone_Damage /2 && Distance < _zone_Damage)
                {
                    Enemy.GetComponent<StatsUnits>()._points_Life = _points_Attack / 4;
                    print("ENEMY LIFE 3 " + Enemy.GetComponent<StatsUnits>()._points_Life);
                }
            }
        }*/
    }

    

    private void OnDestroy()
    {
        //Remover del manager unit
        
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        Destroy(this.gameObject, 5f);
    }

    private void Attack()
    {
        _cooldown_Current -= Time.deltaTime;
        if (_unit_Objective != null)
        {

            //foreach(var unit in _units_InZone)
            //{
            try
            {
                if (_units_InZone.Contains(_unit_Objective) && _unit_Objective.GetComponent<StatsUnits>()._points_Life > 0)
                {
                    if (_cooldown_Current <= 0)
                    {
                        _missile = _pool.Assign_Missile();
                        _missile.Assign_PosAndRot(_missile_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);
                        _missile.Set_Damage(_points_Attack);
                        _missile.Assign_Shooter(this.gameObject);
                        //Debug.Log("Reinicio de reloj");
                        _cooldown_Current = _cooldown_Attack;
                        this.transform.LookAt(_unit_Objective.transform.position);
                    }
                }
            }
            catch { _unit_Objective = _units_InZone[0]; }
            //}
        }
        else
        {
            if(_rocket._enemy_List.Count > 0 && _zone_Rocket)
            {
                try
                {
                    _unit_Objective = _rocket._enemy_List[0].GetComponent<StatsUnits>();
                    if (!_units_InZone.Contains(_unit_Objective))
                    {
                        _ship.OnMove(_unit_Objective.transform.position);
                    }
                }
                catch
                {

                }
                
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {
           // print("Entro naveeeeee");
            //_unit_Objective = other.gameObject.GetComponent<StatsUnits>();
            //_units_InZone.Add(other.gameObject.GetComponent<StatsUnits>());
            if (_unit_Objective != null)
            {
                if (_unit_Objective == other.gameObject.GetComponent<StatsUnits>())
                {
                    print("Entro " + _unit_Objective.name);
                    _ship.OnStop();
                    
                }
            }
        }

        if(other.gameObject.CompareTag("Zone Rocket"))
        {
            _zone_Rocket = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {

            _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());
            if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective)
            {
                _unit_Objective = null;
            }

        }

        if (other.gameObject.CompareTag("Zone Rocket"))
        {
            _zone_Rocket = false;
        }
    }
}
