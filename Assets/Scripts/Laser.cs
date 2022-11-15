using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public float _distance_Laser = 0.5f;
    private bool _laserOn = false;
    public GameObject _objective = null;
    public StatsUnits _Stats;
    public bool Attack;
    private int _damage;
    
    void Start()
    {
        _Stats = GetComponentInParent<StatsUnits>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (_laserOn)
        {
            //print("ENTRE LASER");
            _lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == "Ship Enemy")
                {
                    try
                    {
                        _lineRenderer.SetPosition(1, _objective.transform.position);
                    }
                    catch
                    {

                    }
                    
                    if (Attack)
                    {
                        if (hit.collider.GetComponent<StatsUnits>() == _Stats._unit_Objective)
                        {
                            hit.collider.GetComponent<StatsUnits>().Set_Damage(_Stats._points_Attack);
                            hit.collider.GetComponent<StatsUnits>()._unit_Objective = gameObject.GetComponentInParent<StatsUnits>();
                            
                            //hit.collider.GetComponent<StatsUnits>()._unit_Objective = gameObject.GetComponentInParent<StatsUnits>();
                           // print(hit.collider.gameObject.name);
                            //print(hit.collider.GetComponent<StatsUnits>()._points_Life);
                            Attack = false;
                        }
                    }
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, transform.forward * _distance_Laser);
            }
        }
        else
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    public void SetLaser(bool state)
    {
        _laserOn = state;
    }

    public void SetDamage(int Damage)
    {
        _damage = Damage;
    }
}
