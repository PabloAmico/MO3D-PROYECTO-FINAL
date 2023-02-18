using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWorldDestroy : MonoBehaviour
{
    public float _time_Destroy;
    private float _current_Time = 0;

    void Start()
    {
        
    }


    void Update()
    {
        _current_Time += Time.deltaTime;
    }

    public float Get_Current_Time(){
        return _current_Time;
    }
}
