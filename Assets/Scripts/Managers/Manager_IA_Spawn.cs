using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_IA_Spawn : MonoBehaviour
{

    private Hangar_Enemy[] _hangars;    //Array con los hangares enemigos.
    
    //Asigno los prefab de las naves enemigas. 
    public Ship _ship_Basic;
    public Ship _ship_Missile;
    public Ship _ship_Laser;
    
    private float _time_Spawn_Enemy = 3f;   //Tiempo de spawn de las naves enemigas.
    private float _time_Current_Spawn = 0f; //Tiempo actual del spawn, cuando iguala _time_Spawn_Enemy se instancia una nave.
    private int _current_Hangar = 0;    //Se utiliza para iterar en el array _hangars.
    public int[] _ships;    //Array para saber la cantidad de naves que tengo de cada tipo.
    ManagerUnit _unit;

    void Start()
    {
        _ships = new int[3];    //1 = basic, 2 = laser, 3 = misil.
        foreach(int num in _ships){
            _ships[num] = 0;   
        }
        _hangars = GetComponentsInChildren<Hangar_Enemy>();
        _unit = FindObjectOfType<ManagerUnit>();
    }

    void Update()
    {
        Spawn();
    }

//Este Metodo se utiliza en la clase ManagerGameControl, para ir reduciendo el tiempo de spawn.
    public void Set_Time(float Time){
        _time_Spawn_Enemy = Time;
    }

//Metodo para agregar una nave al array _ships
    public void Add_Ships(int num){
        _ships[num]++;
    }

//Metodo para eliminar una nave del array _ships;
    public void Eliminate_Ship(int num){
        if(_ships[num] > 0){
            _ships[num] -=1;
        }
    }

/*Metodo para crear las naves enemigas
Las naves se van creando dependiendo de la cantidad y tipos de naves creadas con anterioridad*/
    private void Spawn()
    {
        if (_unit._units_Total.Count < _unit._max_Units)    //Si no supere el limite total de naves creadas.
        {
            _time_Current_Spawn += Time.deltaTime;  //Sumo el tiempo transcurrido
            if (_time_Current_Spawn >= _time_Spawn_Enemy)
            {
                _time_Current_Spawn = 0;
                Check_Cant_Ships(); //Llamo al metodo para instanciar las naves enemigas.
                _current_Hangar++;  //Sumo el iterador para ir cambiando el hangar del que instancio.
                if (_current_Hangar >= _hangars.Length) //Si me paso del largo del array lo vuelvo 0.
                {
                    _current_Hangar = 0;
                }
                
            }
        }
    }

   
    //Chequea la cantidad de naves de cada tipo que tengo instanciada y define cual va usarse. Utilizo aleatoriedad para la instanciacion de las naves
    private void Check_Cant_Ships(){    //0 = basic, 1 = laser, 2 = misilles
        if(_ships[0] >= 6 && _ships[2] < _ships[0] / 3){    //Si la nave basica es mayor o igual que 6 y la nave de misiles es menor a un tercio de la basica ingreso al if
            //Instantiate(_ship_Missile, _hangars[_current_Hangar].transform.position, Quaternion.identity);
            float aux = Random.Range(0,10); //Obtengo un numero aleatorio.
                if(aux < 7){
                    Instantiate(_ship_Missile, _hangars[_current_Hangar].transform.position, Quaternion.identity);  //Instancio nave de misiles.
                }else{
                    Instantiate(_ship_Laser, _hangars[_current_Hangar].transform.position, Quaternion.identity);    //Instancio nave de laser.
                }
        }else{
            if(_ships[0] >= 4 && _ships[1] < _ships[0]){    //Si la nave basica es mayor o igual que 4 y la nave laser es menor a la basica ingreso al if
                Random.InitState((int)Time.realtimeSinceStartup);   //Reinicio el Random.
                float aux = Random.Range(0,10); //Obtengo un numero aleatorio.
                if(aux < 7){
                    Instantiate(_ship_Laser, _hangars[_current_Hangar].transform.position, Quaternion.identity);    //Instancio nave de laser.
                }else{
                    Instantiate(_ship_Basic, _hangars[_current_Hangar].transform.position, Quaternion.identity);    //Instancio nave basica.
                }
            }else{
             
                Instantiate(_ship_Basic, _hangars[_current_Hangar].transform.position, Quaternion.identity);    //Instancio nave basica.
               
            }
        }
    }
}
