using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMissile : MonoBehaviour
{

    public GameObject _prefab_Missile = null;
    List<Missile> _pool_Missile = new List<Missile> ();


//Al iniciar tengo misiles creados en la escena, las cuales las asigno a este pool.
    void Start()
    {
        Missile[] Aux = this.gameObject.GetComponentsInChildren<Missile>(); 

        foreach(Missile m in Aux)
        {
            _pool_Missile.Add (m);
            m.gameObject.SetActive(false);
        }
    }



    public Missile Assign_Missile()
    {
          //Devuelvo un misil si hay alguna ya creada sin uso
        foreach(Missile m in _pool_Missile)
        {
            if (!m.gameObject.activeInHierarchy)
            {
                m.gameObject.SetActive(true);
                return m;
            }
        }
//Si no la hay instancio una y la devuelvo.
        Missile Aux = Instantiate(_prefab_Missile.GetComponent<Missile>(), Vector3.zero, Quaternion.identity);
        Aux.gameObject.transform.SetParent(gameObject.transform, true);
        _pool_Missile.Add(Aux);
        return Aux;
    }
}
