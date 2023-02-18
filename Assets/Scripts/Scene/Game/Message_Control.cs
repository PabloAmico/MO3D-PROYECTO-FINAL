using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Message_Control : MonoBehaviour
{
   private bool _have_Message = false;
   private float _time_View = 1.0f;
   private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
          
        
    }

    public void Show_Message(string Message){
       
        //_have_Message = true;
        _text.text = Message;
         //_text.color = new Color(50,50,50,255);
         _text.CrossFadeAlpha(255,0.1f,true);
        print("ENTRE MESSAGE!");
        //if(_have_Message){
        _text.CrossFadeAlpha(0,_time_View,true);
          //  _have_Message = false;
        //}
    }
}
