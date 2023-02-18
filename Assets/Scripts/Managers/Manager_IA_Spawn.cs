using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_IA_Spawn : MonoBehaviour
{

    private Hangar_Enemy[] _hangars;
    public Ship _ship_Basic;
    public Ship _ship_Missile;
    public Ship _ship_Laser;
    
    private float _time_Spawn_Enemy = 5f;
    private float _time_Current_Spawn;
    private int _current_Hangar = 0;
    public int[] _ships;
    ManagerUnit _unit;
    // Start is called before the first frame update
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

    public void Set_Time(float Time){
        _time_Spawn_Enemy = Time;
    }

    public void Add_Ships(int num){
        _ships[num]++;
    }

    public void Eliminate_Ship(int num){
        if(_ships[num] > 0){
            _ships[num] -=1;
        }
    }

    private void Spawn()
    {
        if (_unit._units_Total.Count < _unit._max_Units)
        {
            bool Attacking_hangar = false;
            _time_Current_Spawn += Time.deltaTime;
            if (_time_Current_Spawn >= _time_Spawn_Enemy)
            {
                _time_Current_Spawn = 0;
                foreach (Hangar_Enemy _hangar in _hangars)
                {
                    if (_hangar._is_Attacked)
                    {
                        Check_Cant_Ships();

                        Attacking_hangar = true;
                    }
                }

                if (!Attacking_hangar)
                {
                    Check_Cant_Ships();
                    _current_Hangar++;
                    if (_current_Hangar >= _hangars.Length)
                    {
                        _current_Hangar = 0;
                    }
                }
            }
        }
    }

   
    //Chequea la cantidad de naves de cada tipo que tengo instanciada y define cual va usarse
    private void Check_Cant_Ships(){    //0 = basic, 1 = laser, 2 = misilles
    //print("INGRESE");
        if(_ships[0] >= 6 && _ships[2] < _ships[0] / 3){    
            //Instantiate(_ship_Missile, _hangars[_current_Hangar].transform.position, Quaternion.identity);
            float aux = Random.Range(0,10);
                if(aux < 7){
                    Instantiate(_ship_Missile, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                }else{
                    Instantiate(_ship_Laser, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                }
        }else{
            if(_ships[0] >= 5 && _ships[1] < _ships[0]){
                Random.InitState((int)Time.realtimeSinceStartup);
                float aux = Random.Range(0,10);
                if(aux < 7){
                    Instantiate(_ship_Laser, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                }else{
                    Instantiate(_ship_Basic, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                }
            }else{
                //if(_ships[0] <= _ships[1]){
                    Instantiate(_ship_Basic, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                //}
            }
        }
    }
}
