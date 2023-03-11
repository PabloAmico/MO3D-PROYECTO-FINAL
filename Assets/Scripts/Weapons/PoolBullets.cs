using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBullets : MonoBehaviour
{
    public GameObject _prefab_Bullet = null;
    List<Bullets> _pool_Bullets = new List<Bullets>();

//Al iniciar tengo balas creadas en la escena, las cuales las asigno a este pool.
    void Start()
    {
        Bullets [] Aux = this.gameObject.GetComponentsInChildren<Bullets>();
        foreach (Bullets b in Aux)
        {
            _pool_Bullets.Add(b);
            b.gameObject.SetActive(false);
 
        }

    }


    public Bullets Assign_Bullet()
    {
        //Devuelvo una bala si hay alguna ya creada sin uso
        foreach(Bullets b in _pool_Bullets)
        {

            if (!b.gameObject.activeInHierarchy)
            {
    
                b.gameObject.SetActive(true);
                return b;
            }
        }

//Si no la hay instancio una y la devuelvo.
        Bullets aux = Instantiate(_prefab_Bullet.GetComponent<Bullets>(), Vector3.zero, Quaternion.identity);
        aux.gameObject.transform.SetParent(gameObject.transform, true);
        _pool_Bullets.Add(aux);
        return aux;
    }
}
