using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    int _index_Message = -1;
    string[] _texts;
    public Text _text_Tutorial;
    public Image _image_Tutorial_Ship;
    public bool _enabled_Tutorial = true;

    public Button _continue;
    public Button _play;
    public Toggle _toggle;
    public Image _background;
    // Start is called before the first frame update
    void Start()
    {
        _texts = new string[7];
        //_text_Tutorial = GetComponentInChildren<Text>();
        _enabled_Tutorial = GetComponent<SaveOptions>().Get_Tutorial_Options();
        if(_enabled_Tutorial){
            _play.gameObject.SetActive(false);
            Set_Message();
            Write_Message();
            print("Tiempo parado?");
            Time.timeScale = 0f;
            
        }else{
            _text_Tutorial.gameObject.SetActive(false); 
            _background.gameObject.SetActive(false); 
            _toggle.gameObject.SetActive(false); 
            _continue.gameObject.SetActive(false); 
            _play.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return_Time(){
        Time.timeScale = 1;
    }

    private void Show_Tutorial(){
        _index_Message++;
    }

    private void Set_Message(){
        _texts[0] = "Bienvenido General. Hemos quedado atrapados en este planeta y tu misión es lograr que escapemos. Para ello debemos crear tropas y proteger el cohete hasta que este listo";
        _texts[1] = "Para crear las tropas presiona sobre las imagenes de las misma abajo a la derecha";
        _texts[2] = "";
        _texts[3] = "Cada nave tiene un costo que sera descontado de tu fondo total que esta ubicado arriba a la derecha. parece poco pero no te preocupes, cada segundo se ira sumando dinero gracias a un pequeño recolector que esta dando vueltas por el mundo";
        _texts[4] = "Para seleccionar las tropas puede seleccionar arriba de ellas con un click o mantener apretado boton izquierdo del raton para seleccionar varias. con click derecho las mueves o atacas dependiendo donde lo presiones";
        _texts[5] = "Por último. Para mover la camara lo haces con W, A, S y D. o bien posicionando el raton en el limite de la pantalla";
        _texts[6] = "Muchos exitos general. dependemos de usted.";
    }

    public void Write_Message(){
        if(_index_Message < _texts.Length -1){
            _index_Message++;
            if(_index_Message == 2){
                _image_Tutorial_Ship.enabled = true;
            }else{
                _image_Tutorial_Ship.enabled = false;
            }
        _text_Tutorial.text = _texts[_index_Message];
        }else{
            _continue.gameObject.SetActive(false);
            _play.gameObject.SetActive(true);
        }
       
    }

    public void Start_Game(){
        _text_Tutorial.gameObject.SetActive(false); 
        _background.gameObject.SetActive(false); 
        _toggle.gameObject.SetActive(false); 
        _continue.gameObject.SetActive(false); 
        _play.gameObject.SetActive(false);
         Time.timeScale = 1f;
    }
}
