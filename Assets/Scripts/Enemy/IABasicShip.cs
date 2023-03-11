using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de las naves basicas que disparan balas.
public class IABasicShip : IA_Ship
{
    protected PoolBullets _pool;    //Referencia al pool de balas
    public GameObject _right_Shoot = null;  //referencia al punto de instancia de las balas derechas.
    public GameObject _left_Shoot = null;   //referencia al punto de instancia de las balas izquierdas.
    protected Bullets _bullets_Right;   //Balas que se instancian en la derecha.
    protected Bullets _bullets_Left;    //Balas que se instancian en la izquierda.

    //Metodo que se utiliza para disparar las balas
    protected override void Shoot(Vector3 Objective)
    {
        _cooldown_Current -= Time.deltaTime;

        if (_cooldown_Current <= 0)
        {
            _bullets_Right = _pool.Assign_Bullet();
            _bullets_Right.Assign_PosAndRot(_right_Shoot.transform.position, transform.rotation, Objective);
            _bullets_Right.Set_Damage(_points_Attack);  //El daño se obtiene de la clase StatsUnits
            _bullets_Right.Assign_Shooter(this.gameObject);


            _bullets_Left = _pool.Assign_Bullet();
            _bullets_Left.Assign_PosAndRot(_left_Shoot.transform.position, transform.rotation, Objective);
            _bullets_Left.Set_Damage(_points_Attack);   //El daño se obtiene de la clase StatsUnits
            _bullets_Left.Assign_Shooter(this.gameObject);


            _cooldown_Current = _cooldown_Attack;
            this.transform.LookAt(Objective);   //Apunto la nave hacia el objetivo

        }
    }

    protected override void Initz()
    {
        _pool = FindObjectOfType<PoolBullets>();
        _num_ship = 0;  //se utiliza esta variable para restar en 1 la cantidad de naves de este tipo.
        _manager.Add_Ships(_num_ship);
    }
}
