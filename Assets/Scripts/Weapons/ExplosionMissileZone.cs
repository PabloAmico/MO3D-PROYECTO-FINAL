using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMissileZone : MonoBehaviour
{
    // Start is called before the first frame update
    private List<StatsUnits> _units_Explosion = new List<StatsUnits>();
    private GameObject _ship_Shoot;
    public GameObject _ship_Objective;
    void Start()
    {
        
    }


    public void Set_Shooter(GameObject Shooter)
    {
        _ship_Shoot = Shooter;
    }

    public List<StatsUnits> Get_Explosion()
    {
        return _units_Explosion;
    }

    public void Set_Objective(GameObject Objective)
    {
        _ship_Objective = Objective;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExplosionAttack(GameObject obj)
    {
        float Distance = Vector3.Distance(this.gameObject.transform.position, obj.transform.position);
        float Zone_Damage = gameObject.GetComponent<SphereCollider>().radius;
        if(Distance < Zone_Damage / 3)
        {
            obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 2) ;
            
            print("ENEMY LIFE 1 " + _ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 2);
        }
        else
        {
            if(Distance >= Zone_Damage / 3 && Distance <= Zone_Damage / 2)
            {
                obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 3);
                
                print("ENEMY LIFE 2 " + _ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 3);
            }
            else
            {
                if(Distance > Zone_Damage / 2 && Distance < Zone_Damage)
                {
                    obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 4);
                    
                    print("ENEMY LIFE 3 " + _ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 4);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ship Enemy") && _ship_Shoot.CompareTag("Player"))
        {
            if (other.gameObject != _ship_Objective)
            {
                //print("El objetivo es " + _ship_Objective.name + " la onda le llego a " + other.gameObject.name);
                //print("Entre misil! " + other.gameObject.name + " Position " + transform.position);
               // _units_Explosion.Add(other.gameObject.GetComponent<StatsUnits>());
                ExplosionAttack(other.gameObject);
                //print("Cantidad de enemigos en zona " + _units_Explosion.Count);
            }
        }

        if (other.gameObject.CompareTag("Player") && _ship_Shoot.CompareTag("Ship Enemy"))
        {
            if (other.gameObject != _ship_Objective)
            {
                //print("Entre misil! " + other.gameObject.name + " Position " + transform.position);
                //_units_Explosion.Add(other.gameObject.GetComponent<StatsUnits>());
                ExplosionAttack(other.gameObject);
                //print("Cantidad de enemigos en zona " + _units_Explosion.Count);
            }
        }
    }
}
