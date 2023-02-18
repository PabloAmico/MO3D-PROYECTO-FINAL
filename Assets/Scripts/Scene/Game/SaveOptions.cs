using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptions : MonoBehaviour
{
    private bool _short_Tutorial;

    private string _name_Save = "tutorial";
    private bool _boolData = true;
    private void Awake() {
        bool Have_Data = PlayerPrefs.HasKey(_name_Save);
        if(!Have_Data){
            PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);
        }
        //PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);  //Descomentar esta linea para poder volver a ver el tutorial.
       _short_Tutorial = PlayerPrefs.GetInt(_name_Save) == 1;   //corroboro si el usuario pidio no volver a ver el tutorial. Al compararlo retorna un booleano. Cargo el resultado
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set_View_Tutorial(){
        _boolData = false;
        PlayerPrefs.SetInt(_name_Save, _boolData ? 1:0);
    }

    public bool Get_Tutorial_Options(){
        return _short_Tutorial;
    }
}
