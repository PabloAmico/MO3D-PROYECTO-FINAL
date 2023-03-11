using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShipAttack : StatsUnits
{
    public GameObject _missile_Shoot = null;
    private PoolMissile _pool;  //Pool de mosiles.
    private Missile _missile;   //Misil que instancia la nave.
    private float _zone_Damage; //tamanio de la esfera de explosion.
    private Rocket _rocket;


//Clase heredada de StatsUnits.
    protected override void Init()
    {
        _points_Life_Max = _points_Life;

        _rocket = FindObjectOfType<Rocket>();
        _pool = FindObjectOfType<PoolMissile>();
    }

    void Update()
    {
        Attack();
        //Si la vida es 0
        if(_points_Life <= 0)
        {
            _show_Select.Set_Show(false);   //Dejo de mostrar el cartel de seleccion de naves
            //Destruyo el objeto
            OnDestroy();
        }
    }

   /* public void Set_Zone_Damage(float Zone)
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
        
    }*/

    

    private void OnDestroy()
    {
        //Remover del manager unit
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject, 3f);
    }

    private void Attack()
    {
        //Si la nave se encuentra viva (las naves tardan 5seg en desaparecer, por esto es necesario este if)
        if(_points_Life > 0){  
            _cooldown_Current -= Time.deltaTime;    //Resto el tiempo para disparar.
            if (_unit_Objective != null)    //Si tengo un objetivo asignado.
            {

                try
                {
                    //Y, se encuentra contenida en las unidades dentro de la zona y ademas se encuentra con vida.
                    if (_units_InZone.Contains(_unit_Objective) && _unit_Objective.GetComponent<StatsUnits>()._points_Life > 0)
                    {
                        if (_cooldown_Current <= 0)
                        {
                            _missile = _pool.Assign_Missile();  //Le asigno un misil del pool de misiles
                            _missile.Assign_PosAndRot(_missile_Shoot.transform.position, transform.rotation, _unit_Objective.transform.position);   //Le asigno una posicion y rotacion
                            _missile.Set_Damage(_points_Attack);    //Asigno los puntos de ataques, este atributo se hereda de StatsUnits.
                            _missile.Assign_Shooter(this.gameObject);   //Asigno el tirador.
                            _cooldown_Current = _cooldown_Attack;   //Reinicio el tiempo del disparo
                            this.transform.LookAt(_unit_Objective.transform.position);  //Miro hacia la nave objetivo
                        }
                    }
                }
                catch 
                { 
                    _unit_Objective = _units_InZone[0]; //Sino, cambio de objetivo.
                }
            }
            else
            {
                //Si el enemigo esta disparando al cohete y esta nave se encuentra en la zona cercana al cohete.
                if(_rocket._enemy_List.Count > 0 && _zone_Rocket)
                {
                    try
                    {
                        _unit_Objective = _rocket._enemy_List[0].GetComponent<StatsUnits>();    //Asigno como objetivo a la primer nave que le disparo al cohete.
                        if (!_units_InZone.Contains(_unit_Objective))   //Si no se encuentra cerca de mi rango de disparo.
                        {
                            _ship.OnMove(_unit_Objective.transform.position);   //Me muevo hasta la posicion.
                        }
                    }
                    catch
                    {

                    }
                    
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))
        {
            if (_unit_Objective != null)    //Si no tengo un objetivo asignado
            {
                if (_unit_Objective == other.gameObject.GetComponent<StatsUnits>())
                {
                    _unit_Objective = other.gameObject.GetComponent<StatsUnits>();  //Le asigno la nave enemiga como objetivo.
                    _ship.OnStop(); //Detengo la nave.
                    
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
