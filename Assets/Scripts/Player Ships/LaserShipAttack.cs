using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShipAttack : StatsUnits
{

    public GameObject _right_Shoot = null;  //Posicion derecha en donde se instancia el laser derecho.
    public GameObject _left_Shoot = null;   //Posicion derecha en donde se instancia el laser izquierdo.
    private Rocket _rocket; //Obtengo el cohete desde la escena.
    private Laser[]  _laser;    //Array de laser que contiene la nave.

    public bool _is_Player = true;  //esta variable sirve para distinguir si el laser es disparado por una nave del jugador o por una nave enemiga.
    

//Metodo heredado de StatsUnits
    protected override void Init()
    {
        
        _points_Life_Max = _points_Life;
        _rocket = FindObjectOfType<Rocket>();   //Obtengo el cohete de la escena.
        _laser = GetComponentsInChildren<Laser>();  //Obtengo los laseres del game Object.

        //Le asigno a los laseres que esta nave pertenece al jugador.
        _laser[0]._is_Player = _is_Player;  
        _laser[1]._is_Player = _is_Player;
    }

    void Update()
    {
        //Si la vida es 0
        if (_points_Life <= 0)
        {
            _show_Select.Set_Show(false);   //Dejo de mostrar el cartel de seleccion de naves
            //Desactivo los laseres
            _laser[0].SetLaser(false);
            _laser[1].SetLaser(false);
            //Destruyo el objeto
            OnDestroy();
            
        }
        else
        {
            //Sino, ataco.
            Attack();
        }
    }

//Metodo que se llama cuando se destruye la nave.
    private void OnDestroy()
    {
        //Remover del manager unit
        _ship.RemoveShip(gameObject.GetComponent<Ship>());
        //Desabilito el boxcollider para que las naves enemigas dejen de detectarla como objetivo
        GetComponent<BoxCollider>().enabled = false;    
        Destroy(this.gameObject, 5f);
    }

    private void Attack()
    {
        //Si la nave se encuentra viva (las naves tardan 5seg en desaparecer, por esto es necesario este if)
        if(_points_Life > 0){   
            try{
                if(_unit_Objective != null) //Si tengo un objetivo asignmado.
                {
                    if (_unit_Objective._points_Life <= 0)  //Y ese objetivo NO se encuentra con vida.
                    {
                        _unit_Objective = null; //Vuelvo nulo al objetivo.
                        if(_units_InZone.Count > 0){
                            if (_units_InZone[0] != null)   //Si hay mas unidades en la zona
                            {
                                if (_units_InZone[0]._points_Life > 0)
                                {
                                    _unit_Objective = _units_InZone[0]; //Le asigno como objetivoel primer enemigo de la lista.

                                }
                                else
                                {
                                    if (_units_InZone[1] != null)   //Si el objetivo en la posicion 0 es nula asigno la de la posicion 1.
                                    {
                                        _unit_Objective = _units_InZone[1];
                                    }
                                    else
                                    {   //Sino, deshabilito los laseres
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
                    {   //En cambio si tengo una tropa ya asignada.
                        foreach (var unit in _units_InZone)
                        {
                            //Habilito los laseres
                            _laser[0].SetLaser(true);
                            _laser[1].SetLaser(true);
                            _cooldown_Current -= Time.deltaTime;
                            if (_cooldown_Current <= 0)
                            {   //Si el tiempo es 0, ataco.
                                _laser[0].Attack = true;
                                _laser[1].Attack = true;
                                _cooldown_Current = _cooldown_Attack;   //Reinicio el tiempo.
                            }
                            this.transform.LookAt(_unit_Objective.transform.position);  //Giro la nave para que mire hacia el objetivo.
                        }
                    }

                }
                else
                {
                    _laser[0].SetLaser(false);
                    _laser[1].SetLaser(false);
                }
            }catch{

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy"))  //Si la nave que entra es enemiga
        {
            _unit_Objective = other.gameObject.GetComponent<StatsUnits>();  //La asigno como objetivo
            //Asigno el objetivo a los laseres.
            _laser[0]._objective = _unit_Objective.gameObject;
            _laser[1]._objective = _unit_Objective.gameObject;
            //La agrego a unidades en la zona
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
