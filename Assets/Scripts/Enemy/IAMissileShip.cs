using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMissileShip : IA_Ship
{
    public PoolMissile _pool;
    public GameObject _center_Shoot = null;
    private Missile _missile;
   
    protected override void Shoot(Vector3 Objective)
    {
        _cooldown_Current -= Time.deltaTime;

        if (_cooldown_Current <= 0)
        {
            _missile = _pool.Assign_Missile();
            _missile.Assign_PosAndRot(_center_Shoot.transform.position, transform.rotation, Objective);
            _missile.Set_Damage(_points_Attack);    //El daño se obtiene de la clase StatsUnits
            _missile.Assign_Shooter(this.gameObject);

            _cooldown_Current = _cooldown_Attack;
            this.transform.LookAt(Objective); //Apunto la nave hacia el objetivo
        }
    }

    protected override void Initz()
    {
        _pool = FindObjectOfType<PoolMissile>();
        _num_ship = 2;  //se utiliza esta variable para restar en 1 la cantidad de naves de este tipo.
        _manager.Add_Ships(_num_ship);
    }
}
