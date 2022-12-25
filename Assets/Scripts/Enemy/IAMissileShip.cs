using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMissileShip : IA_Ship
{
    public PoolMissile _pool;
    public GameObject _center_Shoot = null;
    private Missile _missile;
    // Start is called before the first frame update
    protected override void Shoot(Vector3 Objective)
    {
        _cooldown_Current -= Time.deltaTime;

        if (_cooldown_Current <= 0)
        {
            // print("Voy a disparar");
            _missile = _pool.Assign_Missile();
            _missile.Assign_PosAndRot(_center_Shoot.transform.position, transform.rotation, Objective);
            _missile.Set_Damage(_points_Attack);
            _missile.Assign_Shooter(this.gameObject);

            _cooldown_Current = _cooldown_Attack;
            this.transform.LookAt(Objective);
        }
    }

    protected override void Initz()
    {
        _pool = FindObjectOfType<PoolMissile>();
    }
}
