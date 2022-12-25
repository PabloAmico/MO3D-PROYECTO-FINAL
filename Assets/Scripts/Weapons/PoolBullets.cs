using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBullets : MonoBehaviour
{
    public GameObject _prefab_Bullet = null;
    List<Bullets> _pool_Bullets = new List<Bullets>();
    // Start is called before the first frame update
    void Start()
    {
        Bullets [] Aux = this.gameObject.GetComponentsInChildren<Bullets>();
        foreach (Bullets b in Aux)
        {
            _pool_Bullets.Add(b);
            b.gameObject.SetActive(false);
            //Debug.Log(b.name);
        }
        //Debug.Log(_pool_Bullets.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bullets Assign_Bullet()
    {
        foreach(Bullets b in _pool_Bullets)
        {
            //print("Recorri");
            if (!b.gameObject.activeInHierarchy)
            {
                //Debug.Log("Encontre una balita");
                b.gameObject.SetActive(true);
                return b;
            }
        }

        Bullets aux = Instantiate(_prefab_Bullet.GetComponent<Bullets>(), Vector3.zero, Quaternion.identity);
        aux.gameObject.transform.SetParent(gameObject.transform, true);
        _pool_Bullets.Add(aux);
        print("Sin balas");
        return aux;
    }
}
