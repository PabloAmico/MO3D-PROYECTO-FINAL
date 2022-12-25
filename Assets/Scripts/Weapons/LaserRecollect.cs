using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRecollect : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public float _distance_Laser = 0.5f;
    private bool _laserOn = false;
    public GameObject _objective = null;
    // Start is called before the first frame update
    void Start()
    {
     _lineRenderer = GetComponent<LineRenderer>();   
    }

    // Update is called once per frame
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
                    print("COLISION CON LA ROCA");
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
                 print("Les erre, perdon " + hit.collider.name);
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
