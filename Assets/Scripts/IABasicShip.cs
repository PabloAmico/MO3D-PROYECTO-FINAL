using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasicShip : IA_Ship
{
    protected PoolBullets _pool;
    public GameObject _right_Shoot = null;
    public GameObject _left_Shoot = null;
    protected Bullets _bullets_Right;
    protected Bullets _bullets_Left;
    protected override void Shoot(Vector3 Objective)
    {
        _cooldown_Current -= Time.deltaTime;

        if (_cooldown_Current <= 0)
        {
            // print("Voy a disparar");
            _bullets_Right = _pool.Assign_Bullet();
            _bullets_Right.Assign_PosAndRot(_right_Shoot.transform.position, transform.rotation, Objective);
            _bullets_Right.Set_Damage(_points_Attack);
            _bullets_Right.Assign_Shooter(this.gameObject);


            _bullets_Left = _pool.Assign_Bullet();
            _bullets_Left.Assign_PosAndRot(_left_Shoot.transform.position, transform.rotation, Objective);
            _bullets_Left.Set_Damage(_points_Attack);
            _bullets_Left.Assign_Shooter(this.gameObject);


            _cooldown_Current = _cooldown_Attack;
            this.transform.LookAt(Objective);
        }
    }

    protected override void Initz()
    {
        _pool = FindObjectOfType<PoolBullets>();
    }
}
