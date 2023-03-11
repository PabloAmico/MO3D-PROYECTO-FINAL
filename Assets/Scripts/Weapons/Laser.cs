using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lineRenderer; //Linea que dibuja el laser.
    public float _distance_Laser = 0.5f;    //Distancia del laser.
    private bool _laserOn = false;
    public GameObject _objective = null;
    public StatsUnits _Stats;
    public bool Attack;
    public bool _is_Player; //Si es del jugador el laser disparado.
    
    void Start()
    {
        _Stats = GetComponentInParent<StatsUnits>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (_laserOn)   //Si el laser esta activo
        {
            
            _lineRenderer.SetPosition(0, transform.position);   //Seteo el inicio en la posicion inicial.
            RaycastHit hit; //Y creo un raycast.
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if(_is_Player){ //Si es una nave del jugador
                    if (hit.collider.tag == "Ship Enemy")   //Y el raycast colisiona con una nave enemiga.
                    {
                        try
                        {
                            _lineRenderer.SetPosition(1, _objective.transform.position);    //Pongo el fin del laser en la nave objetivo.
                        }
                        catch
                        {

                        }
                        
                        //Ejecuto el daño de ataque.
                        if (Attack)
                        {
                            if (hit.collider.GetComponent<StatsUnits>() == _Stats._unit_Objective)
                            {
                                hit.collider.GetComponent<StatsUnits>().Set_Damage(_Stats._points_Attack);
                                hit.collider.GetComponent<StatsUnits>()._unit_Objective = gameObject.GetComponentInParent<StatsUnits>();
                                Attack = false;
                            }
                        }
                    }
                }else{
                    if (hit.collider.tag == "Player")   //Si colisiono con el jugador
                    {
                        try
                        {
                            _lineRenderer.SetPosition(1, _objective.transform.position);    //Establezco el fin en el objetivo.
                            
                        }
                        catch
                        {

                        }
                        //Ejecuto el daño de ataque.
                        if (Attack)
                        {
                            if (hit.collider.GetComponent<StatsUnits>() == _Stats._unit_Objective)
                            {
                                hit.collider.GetComponent<StatsUnits>().Set_Damage(_Stats._points_Attack);
                                hit.collider.GetComponent<StatsUnits>()._unit_Objective = gameObject.GetComponentInParent<StatsUnits>();
                                Attack = false;
                            }
                        }
                    }
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, transform.forward * _distance_Laser);
            }
        }
        else//Si no tengo objetivo. Hago desaparecer el laser.
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    public void SetLaser(bool state)
    {
        _laserOn = state;
    }
}
