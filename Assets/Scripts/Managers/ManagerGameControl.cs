using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameControl : MonoBehaviour
{
    ManagerWorldDestroy _world = null;
    Rocket _rocket = null;
    ManagerMeteorite _meteorite = null;
    Manager_IA_Spawn _IA_Spawn = null;
    bool _win_Game = false;
    bool _lose_Game = false;
    bool _time_Over = false;

    private Light _light;

    void Start()
    {
        _light = FindObjectOfType<Light>();
        _world = gameObject.GetComponent<ManagerWorldDestroy>();
        _rocket = FindObjectOfType<Rocket>();
        _meteorite = FindObjectOfType<ManagerMeteorite>();
        _IA_Spawn = FindObjectOfType<Manager_IA_Spawn>();
    }

    void Update()
    {
        Check_Lose();
        Check_Win();
        Check_Speed_Meteorite();
    }

    private void Check_Win(){
        if(_rocket.Get_Percentage() >= 100){
            _win_Game = true;
        }
    }

    private void Check_Lose(){
        if(_rocket.Get_Percentage() <=0){
            _lose_Game = true;
            _time_Over = false;
        }else{
            if(_world.Get_Current_Time() >= _world._time_Destroy){
                _lose_Game = true;
                _time_Over = true;
            }
        }
    }

    private void Check_Speed_Meteorite(){
        if(_world.Get_Current_Time() > _world._time_Destroy / 1.1f && _meteorite.Get_Time_Create() != 1.5f){ //Ultimo minuto
            _meteorite.Set_Time(1.5f);
            _IA_Spawn.Set_Time(1.5f);
            _light.color = new Color(212,98,85,255);
        }else{
            if(_world.Get_Current_Time() > _world._time_Destroy / 1.5f && _meteorite.Get_Time_Create() != 3f){
                _meteorite.Set_Time(3f);
                _IA_Spawn.Set_Time(2.5f);
                _light.color = new Color(215,135,64,255);
            }else{
                if(_world.Get_Current_Time() > _world._time_Destroy / 2f && _meteorite.Get_Time_Create() != 4.5f){
                    _meteorite.Set_Time(4.5f);
                    _IA_Spawn.Set_Time(3f);
                    _light.color = new Color(217,172,75,255);
                }
            }
        }
    }
}


