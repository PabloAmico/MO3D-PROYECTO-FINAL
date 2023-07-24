using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDestructionWorld : MonoBehaviour
{
    Text _text; //texto para mostrar el tiempo que falta para que se destruya el mundo.
    ManagerWorldDestroy _world;
    SaveOptions _saveOptions;
    void Start()
    {
        _saveOptions = FindObjectOfType<SaveOptions>();
        _text = GetComponent<Text>();
        _world = FindObjectOfType<ManagerWorldDestroy>();
    }

    void Update()
    {
        if (!_saveOptions.Get_English_Options())
        {
            _text.text = "" + (int)(_world._time_Destroy - _world.Get_Current_Time()) + " segundos";    //Muestro el tiempo.
        }
        else
        {
            _text.text = "" + (int)(_world._time_Destroy - _world.Get_Current_Time()) + " seconds";    //Muestro el tiempo.
        }
    }
}
