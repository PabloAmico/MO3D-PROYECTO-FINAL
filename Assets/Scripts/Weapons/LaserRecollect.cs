using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRecollect : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public float _distance_Laser = 0.5f;
    private bool _laserOn = false;
    public GameObject _objective = null;
    void Start()
    {
     _lineRenderer = GetComponent<LineRenderer>();   
    }


//Este laser funciona igual que el anterior pero solo detecta las rocas.
    void Update()
    {
        if (_laserOn)
        {
            _lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == "RockOfMoney")
                {
                    try
                    {
                        _lineRenderer.SetPosition(1, _objective.transform.position);
                    }
                    catch
                    {

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

    public void SetObjective(GameObject obj){
        _objective = obj;        
    }
}
