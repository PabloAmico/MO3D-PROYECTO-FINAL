using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDestructionWorld : MonoBehaviour
{
    Text _text; //texto para mostrar el tiempo que falta para que se destruya el mundo.
    ManagerWorldDestroy _world;
    void Start()
    {
        _text = GetComponent<Text>();
        _world = FindObjectOfType<ManagerWorldDestroy>();
    }

    void Update()
    {
        _text.text = "" + (int)(_world._time_Destroy - _world.Get_Current_Time()) + " segundos";    //Muestro el tiempo.
    }
}
