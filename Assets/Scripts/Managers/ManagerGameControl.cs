using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManagerGameControl : MonoBehaviour
{
    ManagerWorldDestroy _world = null;  //Manager del Mundo
    Rocket _rocket = null;  //Cohete de despegue
    ManagerMeteorite _meteorite = null; //Manager del meteorito
    Manager_IA_Spawn _IA_Spawn = null;  //Manager del spawn de naves enemigas
    ChangeCameraControl _cam;   //Control de las camaras.

    //variables de condicion de victoria y derrota.
    bool _win_Game = false;
    bool _lose_Game = false;
    bool _time_Over = false;

    //Variables que se utilizan para las animaciones de fin de juego.
    float _time_Explosion_Rocket = 0f;
    float _time_Scene_Explosion = 6f;

    

    //Luz y variables de manejo de luz
    private Light _light;
    private bool light_1, light_2, light_3 = false; //se utilizan para setear solo una vez el color de las luces.

    void Start()
    {
        //Obtengo de la escena los distintos GameObjects. 
        _cam = FindObjectOfType<ChangeCameraControl>();
        _light = FindObjectOfType<Light>();
        _world = gameObject.GetComponent<ManagerWorldDestroy>();
        _rocket = FindObjectOfType<Rocket>();
        _meteorite = FindObjectOfType<ManagerMeteorite>();
        _IA_Spawn = FindObjectOfType<Manager_IA_Spawn>();
        
    }

    void Update()
    {
        Check_Destroy_World();
        Check_Win();
        Check_Speed_Meteorite();
        if(_win_Game && _rocket.transform.position.y >= 100f){  //Si el cohete despego y se encvuentra a 100f de altura cambio de escena
            SceneManager.LoadScene("SceneWin");
        }else{ //sino
            if(_lose_Game && !_time_Over){  //si perdi por destruccion de la nave 
                _time_Explosion_Rocket -= Time.deltaTime;
                _time_Scene_Explosion -= Time.deltaTime;
                if(_time_Explosion_Rocket <= 0){
                    _rocket.Particles_Destroy();    //activo las particulas de explosion del cohete
                    _time_Explosion_Rocket = 1.2f;
                }else{
                    if(_time_Scene_Explosion <= 0){ //si el tiempo de animacion de explosion se termino, cambio de escena
                        SceneManager.LoadScene("SceneLose");
                    }
                }
            }else{
                if(_time_Over){ //Si se termino el tiempo del mundo, cambio de escena.
                    SceneManager.LoadScene("SceneLose");
                }
            }
        }
    }

//Metodo para chequear el estado de victoria (al completar el 100% del cohete).
    private void Check_Win(){
        if(_rocket.Get_Percentage() >= 100){
            _win_Game = true;
        }
    }


//Metodo para chequear el estado de derrota por destruccion del mundo.
    private void Check_Destroy_World(){
        if(_world.Get_Current_Time() >= _world._time_Destroy){
            _lose_Game = true;
            _time_Over = true;
        }
        
    }


//Metodo para chequear el estado de derrota por destruccion del cohete.
    public void Check_Destroy_Rocket(){
         if(_rocket.Get_Percentage() <=0){  //Si es 0%
            _rocket.Remove_Bar_Percentage();    //Remuevo la barra de porcentaje del cohete.
            _rocket.Sound_Explosion();  //Activo el sonido de explosion
            _cam.Set_Camera(1); //Cambio la camara para enfocar el cohete de frente.
            _lose_Game = true;
            _time_Over = false;
        }
    }

//Metodo que cambia la velocidad de creacion de los meteoritos y naves enemigas dependiendo del tiempo que llevamos en el juego.
    private void Check_Speed_Meteorite(){
        if(_world.Get_Current_Time() >= (_world._time_Destroy / 1.1f) && _meteorite.Get_Time_Create() != 1.5f){ //Ultimo minuto
            _meteorite.Set_Time(1.5f);  //reduzco el tiempo de creacion de los meteoritos.
            _IA_Spawn.Set_Time(1.5f);   //reduzco el tiempo de creacion de las naves enemigas.
             if(!light_1){
                _light.color = new Color(0.831f,0.384f,0.333f,1);   //Cambio el color de la iluminacion
                light_1 = true;
             }
        }else{
            if(_world.Get_Current_Time() >= (_world._time_Destroy / 1.5f) && _meteorite.Get_Time_Create() != 3f){
                _meteorite.Set_Time(3f);    //reduzco el tiempo de creacion de los meteoritos.
                _IA_Spawn.Set_Time(2.5f);   //reduzco el tiempo de creacion de las naves enemigas.
                if(!light_2){
                    _light.color = new Color(0.843f,0.53f,0.251f,1);    //Cambio el color de la iluminacion
                    light_2 = true;
                }
            }else{
                if(_world.Get_Current_Time() > _world._time_Destroy / 2f && _meteorite.Get_Time_Create() != 4.5f){
                    _meteorite.Set_Time(4.5f);  //reduzco el tiempo de creacion de los meteoritos.
                    _IA_Spawn.Set_Time(3f); //reduzco el tiempo de creacion de las naves enemigas.
                    if(!light_3){
                        _light.color = new Color(0.85f,0.675f,0.29f,1); //Cambio el color de la iluminacion
                        light_3 = true;
                    }
                }
            }
        }
    }
}


