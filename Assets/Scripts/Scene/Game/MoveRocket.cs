using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Metodo que se utiliza para mover el cohete al ganar la partida.
public class MoveRocket : MonoBehaviour
{

    private Rocket _rocket;

    public bool _move_Rocket = false;

    public float _cooldown_Move = 5f;   //Tiempo de espera para moverlo.

    public float _speed_Move = 0f;  //Velocidad de movimiento.

    public bool _first_Change_Camera = true;

    ChangeCameraControl _cam;
    void Start()
    {
        _cam = FindObjectOfType<ChangeCameraControl>();
        _rocket = gameObject.GetComponent<Rocket>();
    }

    
    void Update()
    {
        if(_move_Rocket && _cooldown_Move >= 0f){   //Si todavia no es tiempo de que se mueva.
            _cooldown_Move -= Time.deltaTime;   //Resto el tiempo.
        }else{
            if(_move_Rocket){
                _speed_Move += 5*Time.deltaTime;    //Multiplico 5 por el tiempo, para ir teniendo un despegue exponencial.
                transform.position = new Vector3(transform.position.x,transform.position.y + _speed_Move*Time.deltaTime , transform.position.z);    //Voy moviendo el cohete
                if(transform.position.y >=20f && _first_Change_Camera){ //Si ya no se muestra en camara
                    _first_Change_Camera = false;   
                    _cam.Set_Camera(2); //Cambio de camara.
                }
            }
        }
    }
}
