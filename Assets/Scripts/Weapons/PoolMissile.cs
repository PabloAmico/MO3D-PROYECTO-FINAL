using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMissile : MonoBehaviour
{

    public GameObject _prefab_Missile = null;
    List<Missile> _pool_Missile = new List<Missile> ();
    // Start is called before the first frame update
    void Start()
    {
        Missile[] Aux = this.gameObject.GetComponentsInChildren<Missile>(); 

        foreach(Missile m in Aux)
        {
            _pool_Missile.Add (m);
            m.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Missile Assign_Missile()
    {
        foreach(Missile m in _pool_Missile)
        {
            if (!m.gameObject.activeInHierarchy)
            {
                m.gameObject.SetActive(true);
                return m;
            }
        }

        Missile Aux = Instantiate(_prefab_Missile.GetComponent<Missile>(), Vector3.zero, Quaternion.identity);
        Aux.gameObject.transform.SetParent(gameObject.transform, true);
        _pool_Missile.Add(Aux);
        return Aux;
    }
}
