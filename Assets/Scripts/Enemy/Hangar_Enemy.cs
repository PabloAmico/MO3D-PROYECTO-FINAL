using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar_Enemy : MonoBehaviour
{

    public bool _is_Attacked = false;
    public Vector3 _position_Spawn;
    public float _life = 2500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Damage(int Damage){
        _life -= Damage;
        print("Daño recibido, vida total : " + _life);
    }
}
