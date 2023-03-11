using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//Clase que controla los mensajes que aparecen abajo en la pantalla
public class Message_Control : MonoBehaviour
{
   private bool _have_Message = false;  //Si hay un mensaje escrito es True
   private float _time_View = 1.0f;
   private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

//Metodo que muestra un mensaje.
    public void Show_Message(string Message){
        _text.text = Message;   //Recibe el mensaje y lo asigna
         _text.CrossFadeAlpha(255,0.1f,true);   //Lo cambia de color.
        _text.CrossFadeAlpha(0,_time_View,true);    //Lo hace desaparecer.
    }
}
