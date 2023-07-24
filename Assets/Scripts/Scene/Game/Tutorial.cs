using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

public class Tutorial : MonoBehaviour
{

    int _index_Message = -1;    //Numero del mensaje.
    string[] _texts;    //Array de textos.
    public Text _text_Tutorial; //Texto en el que se muestra el tutorial.
    public Image _image_Tutorial_Ship;  //Imagen de las naves.
    
    public Sprite _sprite_Tutorial_English;
    public Sprite _sprite_Tutorial_Spanish;
    public bool _enabled_Tutorial = true;   //Si el tutoriual esta habilitado es true.
    public bool _language_English = false;

    public Button _continue;    //Boton de continuar
    public Button _play;    //boton para empezar a jugar.
    public Toggle _toggle;  //Boton para saltar el tutorial la proxima vez que se juegue.
    public Image _background;   //Imagen gris del fondo.
    public SaveOptions _save;
    
    void Start()
    {
        _save = GetComponent<SaveOptions>();
        _texts = new string[8];
        _save.Get_English_Options();
        _enabled_Tutorial = _save.Get_Tutorial_Options();
        _language_English = _save.Get_English_Options();
        if(_enabled_Tutorial){  //Si el tutorial esta activo, lo muestro.
            _play.gameObject.SetActive(false);
            Set_Message();
            Write_Message();
            Time.timeScale = 0f;    //Detengo el tiempo en el juego.
            if (_language_English)
            {
                _toggle.GetComponentInChildren<Text>().text = "DON´T SHOW AGAIN";
            }
            else
            {
                _toggle.GetComponentInChildren<Text>().text = "NO VOLVER A MOSTRAR EL TUTORIAL";
            }
            
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
        print(_save.Get_English_Options());
        if (!_language_English)
        {
            
            _texts[0] = "Bienvenido General. Hemos quedado atrapados en este planeta al borde de la destrucción. Tu misión es lograr que escapemos. Para ello debemos crear tropas y proteger el cohete hasta que este listo para despegar";
            _texts[1] = "Sí, como hás leido bien. Este planeta esta próximo a su destrucción. Arriba verás cuántos segundos faltan para que esto suceda. Que no te extrañe ver meteoritos. Eso si, intenta que no caigan sobre tus naves.";
            _texts[2] = "Para crear las tropas presiona sobre las imágenes de las misma abajo a la derecha";
            _texts[3] = "";
            _texts[4] = "Cada nave tiene un costo que será descontado de tu fondo total que esta ubicado arriba a la derecha. parece poco pero no te preocupes, cada segundo se ira sumando dinero gracias a un pequeño recolector que está dando vueltas por el mundo";
            _texts[5] = "Para seleccionar las tropas puede seleccionar arriba de ellas con un click o mantener apretado botón izquierdo del ratón para seleccionar varias. con click derecho las mueves o atacas dependiendo donde lo presiones";
            _texts[6] = "Por último. Para mover la cámara lo haces con W, A, S y D. o bien posicionando el ratón en el limite de la pantalla. Con la tecla espacio la cámara se centra en el cohete";
            _texts[7] = "Muchos éxitos general. dependemos de usted.";
        }
        else
        {
            _texts[0] = "Welcome, General. We have been trapped on this planet on the verge of destruction. Your mission is to set us free. You must create troops and protect the rocket until it’s ready to take off.";
            _texts[1] = "Yes, as you see, this planet is about to explode. Up on the screen you will see how many seconds are left for this to happen. Don't be surprised to see meteorites. Let’s hope they don’t fall upon us.";
            _texts[2] = "To create the troops, click on the images below on your right.";
            _texts[3] = "";
            _texts[4] = "Each vessel has a cost that will be deducted from your total fund, which is located at the top right. It seems very little, but don't worry, money will be added every second, thanks to a small ship that is collecting stones around the world.";
            _texts[5] = "To select troops, use the left button of your mouse. Click to select one, or hold down and drag for several troops. With the right click you either move or attack, depending on where you press.";
            _texts[6] = "Finally, you can adjust your vision using W-A-S & D, or by placing the pointer at the edge of the screen. Use the space key to focus the camera on the rocket.";
            _texts[7] = "Good luck, General. You are our last hope.";


        }
    }

//Metodo para ir cambiando los mensajes.
    public void Write_Message(){
        print ("PRESIONADO");
        if(_index_Message < _texts.Length -1){ 
            _index_Message++;
            if(_index_Message == 3){
                if (_language_English)
                {
                    _image_Tutorial_Ship.sprite = _sprite_Tutorial_English;
                }
                else
                {
                    _image_Tutorial_Ship.sprite = _sprite_Tutorial_Spanish;
                }
                _image_Tutorial_Ship.enabled = true;//muestro la imagen del tutorial.
            }
            else{
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
