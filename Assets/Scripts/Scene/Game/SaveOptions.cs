using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se utiliza para saltar el tutorial.
public class SaveOptions : MonoBehaviour
{
    private bool _short_Tutorial;   //Booleano que si es verdadero salta el tutorial.

    private string _name_Save = "tutorial"; //nombre con el que se guarda la respuesta del jugador.
    private bool _boolData = true;
    private void Awake() {
        bool Have_Data = PlayerPrefs.HasKey(_name_Save);    //obtengo la respuesta anterior del usuario.
        if(!Have_Data){
            PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);    //Si no tengo dato lo asigno dependiendo de lo que eligio el usuario.
        }
        //PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);  //Descomentar esta linea para poder volver a ver el tutorial.
       _short_Tutorial = PlayerPrefs.GetInt(_name_Save) == 1;   //corroboro si el usuario pidio no volver a ver el tutorial. Al compararlo retorna un booleano. Cargo el resultado
    }

//Metodo que se utiliza para guardar la opcion del jugador y no volver a mostrar el tutorial..
    public void Set_View_Tutorial(){
        _boolData = false;
        PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);
    }

    public bool Get_Tutorial_Options(){
        return _short_Tutorial;
    }
}
