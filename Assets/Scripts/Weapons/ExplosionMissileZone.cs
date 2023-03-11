using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMissileZone : MonoBehaviour
{
  //Lista con los objetivos que alcanzo la explosion.
    private List<StatsUnits> _units_Explosion = new List<StatsUnits>();
    private GameObject _ship_Shoot;
    public GameObject _ship_Objective;


    public void Set_Shooter(GameObject Shooter)
    {
        _ship_Shoot = Shooter;
    }


//Devuelve las unidades que recibieron la explosion.
    public List<StatsUnits> Get_Explosion()
    {
        return _units_Explosion;
    }

    public void Set_Objective(GameObject Objective)
    {
        _ship_Objective = Objective;
    }


//Metodo que realiza la explosion. A medida que se aleja mas del centro el daño disminuye.
    private void ExplosionAttack(GameObject obj)
    {
        float Distance = Vector3.Distance(this.gameObject.transform.position, obj.transform.position);
        float Zone_Damage = gameObject.GetComponent<SphereCollider>().radius;
        if(Distance < Zone_Damage / 3)
        {
            obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 2) ;
        }
        else
        {
            if(Distance >= Zone_Damage / 3 && Distance <= Zone_Damage / 2)
            {
                obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 3);
                
            }
            else
            {
                if(Distance > Zone_Damage / 2 && Distance < Zone_Damage)
                {
                    obj.GetComponent<StatsUnits>().Set_Damage(_ship_Shoot.GetComponent<StatsUnits>()._points_Attack / 4);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        try{
            if (other != null){
                if(other.gameObject.CompareTag("Ship Enemy") && _ship_Shoot.CompareTag("Player"))
                {
                    if (other.gameObject != _ship_Objective)
                    {

                        ExplosionAttack(other.gameObject);

                    }
                }

                if (other.gameObject.CompareTag("Player") && _ship_Shoot.CompareTag("Ship Enemy"))
                {
                    if (other.gameObject != _ship_Objective)
                    {

                        ExplosionAttack(other.gameObject);
                    }
                }
            }
        }catch{

        }
    
    }
}
