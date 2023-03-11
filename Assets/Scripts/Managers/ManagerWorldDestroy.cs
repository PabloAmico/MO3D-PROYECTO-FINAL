using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWorldDestroy : MonoBehaviour
{
    public float _time_Destroy; //Tiempo total de destruccion
    private float _current_Time = 0;    //Tiempo transcurrido.

    void Update()
    {
        _current_Time += Time.deltaTime;    //Le sumo el tiempo al tiempo transcurrido
    }

//Metodo para obtener el tiempo transcurrido. Se utiliza en ManagerGameControl.
    public float Get_Current_Time(){   
        return _current_Time;
    }
}
