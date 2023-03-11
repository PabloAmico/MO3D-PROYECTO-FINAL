using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se utiliza en los hangares enemigos. 
//En un principio iban a poder ser atacados, pero por cuestion de tiempo esta mecanica no fue aplicada en esta etapa del proyecto.
public class Hangar_Enemy : MonoBehaviour
{

    public bool _is_Attacked = false;
    public Vector3 _position_Spawn; //Posicion en donde van aparecer las naves construidas en este hangar.
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
