using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShipAttack : StatsUnits
{
    public GameObject _right_Shoot = null;  //Punto de instancia de la bala derecha.
    public GameObject _left_Shoot = null;   //Punto de instancia de la bala izquierda.
    private PoolBullets _pool;  //Pool de balas.
    Bullets _bullets_Right; //Bala que se instancia en la derecha.
    Bullets _bullets_Left;  //Bala que se instancia en la izquierda.
    private Rocket _rocket;

    void Update()
    {
        Attack();
        if(_points_Life <= 0)
        {
            _show_Select.Set_Show(false);   //dejo de mostrar la marca de seleccion.
            OnDestroy();
        }

        
        
    }

    private void OnDestroy()
    {
        //Remover del manager unit
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        _ship.OnStop(); //Detengo la nave
         GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject, 3f);   //La destruyo luego de 3 segundos
    }

    //Metodo heredado de Stats_Units.
    protected override void Init()
    {
        _points_Life_Max = _points_Life;
        _rocket = FindObjectOfType<Rocket>();
        _pool = FindObjectOfType<PoolBullets>();
    }

   

    private void Attack()
    {
        if(_points_Life > 0){   //Si mi nave esta viva puede atacar.
            _cooldown_Current -= Time.deltaTime;
            if (_unit_Objective != null)    //Si tengo un objetivo al cual atacar.
            {
                foreach (var unit in _units_InZone) //Recorro las unidades que tengo en mi zona de ataque.
                {

                    if (_unit_Objective.GetComponent<StatsUnits>()._points_Life > 0)    //Y si esa unidad sigue viva.
                    {
                        if (_cooldown_Current <= 0)
                        {
                            _bullets_Right = _pool.Assign_Bullet();
                            _bullets_Right.Assign_PosAndRot(_right_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);
                            _bullets_Right.Set_Damage(_points_Attack);  //El daño se obtiene de la clase StatsUnits.
                            _bullets_Right.Assign_Shooter(this.gameObject);


                            _bullets_Left = _pool.Assign_Bullet();
                            _bullets_Left.Assign_PosAndRot(_left_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);
                            _bullets_Left.Set_Damage(_points_Attack);   //El daño se obtiene de la clase StatsUnits.
                            _bullets_Left.Assign_Shooter(this.gameObject);


                            _cooldown_Current = _cooldown_Attack;
                            this.transform.LookAt(_unit_Objective.transform.position);  //Apunto la nave hacia el objetivo.
                        }
                    }
                }
            }
            else
            {
                if (_rocket._enemy_List.Count > 0 && _zone_Rocket)  //Si la nave se encuentra en la zona del cohete y la estan atacando
                {
                    _unit_Objective = _rocket._enemy_List[0].GetComponent<StatsUnits>();    //Le asigno como objetivo al primer enemigo que haya atacado el cohete
                    if (!_units_InZone.Contains(_unit_Objective) && _unit_Objective.GetComponent<IABasicShip>()._points_Life > 0)
                    {
                        _ship.OnMove(_unit_Objective.transform.position);   //Me muevo hasta este objetivo.
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))  //Si una nave enemiga entra en la zona
        {
            _unit_Objective = other.gameObject.GetComponent<StatsUnits>();  //La asigno como objetivo.
            _units_InZone.Add(other.gameObject.GetComponent<StatsUnits>()); //Y la agrego a las unidades en mi zona.
            if (_unit_Objective != null)    //Si tengo un objetivo
            {
                if (_unit_Objective == other.gameObject.GetComponent<StatsUnits>() && _ship._select_Attack)
                {
                    _ship.OnStop(); //Detengo la nave
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
        if (other.gameObject.CompareTag("Ship Enemy"))  //Si una nave enemiga sale de la zona
        {
            if (other.gameObject != null)
            {
               
                _units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());  //La elimino de las unidades en mi zona
                if (other.gameObject.GetComponent<StatsUnits>() == _unit_Objective) //Y era mi objetivo la saco de mis objetivos
                {
                    _unit_Objective = null;
                    if(_units_InZone.Count > 0){
                        _unit_Objective = _units_InZone[0];
                    }
                }
            }

        }

        if (other.gameObject.CompareTag("Zone Rocket"))
        {
            _zone_Rocket = false;
        }
    }
}
