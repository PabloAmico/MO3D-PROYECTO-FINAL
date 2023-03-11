using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    int _index_Message = -1;    //Numero del mensaje.
    string[] _texts;    //Array de textos.
    public Text _text_Tutorial; //Texto en el que se muestra el tutorial.
    public Image _image_Tutorial_Ship;  //Imagen de las naves.
    public bool _enabled_Tutorial = true;   //Si el tutoriual esta habilitado es true.

    public Button _continue;    //Boton de continuar
    public Button _play;    //boton para empezar a jugar.
    public Toggle _toggle;  //Boton para saltar el tutorial la proxima vez que se juegue.
    public Image _background;   //Imagen gris del fondo.
    
    void Start()
    {
        _texts = new string[8];
       
        _enabled_Tutorial = GetComponent<SaveOptions>().Get_Tutorial_Options();
        if(_enabled_Tutorial){  //Si el tutorial esta activo, lo muestro.
            _play.gameObject.SetActive(false);
            Set_Message();
            Write_Message();
            Time.timeScale = 0f;    //Detengo el tiempo en el juego.
            
        }else{  //Sino desactivo todo lo relacionado con el tutorial.
            _text_Tutorial.gameObject.SetActive(false); 
            _background.gameObject.SetActive(false); 
            _toggle.gameObject.SetActive(false); 
            _continue.gameObject.SetActive(false); 
            _play.gameObject.SetActive(false);
        }
    }

//Metodo para retornar el tiempo a su normalidad.
    public void Return_Time(){
        Time.timeScale = 1;
    }

//Cambia el index para ir mostrando los mensajes siguientes.
    private void Show_Tutorial(){
        _index_Message++;
    }


//Mensajes que se muestran en pantalla.
    private void Set_Message(){
        _texts[0] = "Bienvenido General. Hemos quedado atrapados en este planeta al borde de la destruccion y tu misión es lograr que escapemos. Para ello debemos crear tropas y proteger el cohete hasta que este listo";
        _texts[1] = "Si, como has leido bien. Este mundo esta proximo a su destrucción. Arriba veras cuantos segundos faltan para que esto suceda. Que no te extrañe ver meteoritos. Eso si, intenta que no caigan sobre tus naves.";
        _texts[2] = "Para crear las tropas presiona sobre las imagenes de las misma abajo a la derecha";
        _texts[3] = "";
        _texts[4] = "Cada nave tiene un costo que sera descontado de tu fondo total que esta ubicado arriba a la derecha. parece poco pero no te preocupes, cada segundo se ira sumando dinero gracias a un pequeño recolector que esta dando vueltas por el mundo";
        _texts[5] = "Para seleccionar las tropas puede seleccionar arriba de ellas con un click o mantener apretado boton izquierdo del raton para seleccionar varias. con click derecho las mueves o atacas dependiendo donde lo presiones";
        _texts[6] = "Por último. Para mover la camara lo haces con W, A, S y D. o bien posicionando el raton en el limite de la pantalla. Con la tecla espacio la camara se centra en el cohete";
        _texts[7] = "Muchos exitos general. dependemos de usted.";
    }

//Metodo para ir cambiando los mensajes.
    public void Write_Message(){
        if(_index_Message < _texts.Length -1){ 
            _index_Message++;
            if(_index_Message == 3){   
                _image_Tutorial_Ship.enabled = true;    //muestro la imagen del tutorial.
            }else{
                _image_Tutorial_Ship.enabled = false;
            }
        _text_Tutorial.text = _texts[_index_Message];
        }else{
            _continue.gameObject.SetActive(false);
            _play.gameObject.SetActive(true);
        }
       
    }

//Al comenzar el juego desabhilito todo lo corespondiente al tutorial y vuelvo el tiempo a su normalidad.
    public void Start_Game(){
        _text_Tutorial.gameObject.SetActive(false); 
        _background.gameObject.SetActive(false); 
        _toggle.gameObject.SetActive(false); 
        _continue.gameObject.SetActive(false); 
        _play.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
