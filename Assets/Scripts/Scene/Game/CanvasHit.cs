using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//Canvas que muestra el daño recibido por las naves.
public class CanvasHit : MonoBehaviour
{
    ChangeCameraControl _cam;
    public TextHit _text_Prefab; 
    public string Name;
    private TextHit _text_Instantiate = null;   //Texto que se instancia.
    void Start()
    {
        _cam = FindObjectOfType<ChangeCameraControl>();
    }

    void Update()
    {
        UI_Look_Camera();
    }


//Metodo que muestra en un texto cada vez que recibe daño.
    public void Create_Text(int life)
    {
        if (_text_Instantiate == null)  //Si el texto es igual a nulo.
        {
            try
            {
                _text_Instantiate = Instantiate(_text_Prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);   //Instancio el texto
                _text_Instantiate.transform.parent = gameObject.transform;  //Lo hago hijo de este objeto.
                
                //Le asigno un texto.
                _text_Instantiate.Set_Text(life, gameObject.GetComponentInParent<StatsUnits>().name, gameObject.GetComponent<CanvasHit>());

                //Le asigno la altura maxima hasta la que va a moverse y le digo que se mueva.
                _text_Instantiate._max_Height = gameObject.GetComponent<RectTransform>().anchorMax.y + transform.position.y;
                _text_Instantiate._move = true;
            }
            catch { }
        }
        else
        {
            //Sino le sumo mas daño al texto.
            _text_Instantiate.Set_Text(life, gameObject.GetComponentInParent<StatsUnits>().name, gameObject.GetComponent<CanvasHit>());
        }
        
    }

    public void Destroy_Text()
    {
        _text_Instantiate = null;
    }


//Metodo para hacer que el texto siempre mire hacia la camara.
    private void UI_Look_Camera()
    {
        try{
            if(_cam._array_Camera[0].activeSelf){   //si la camara pirncipal esta activa.
            transform.forward = _cam._array_Camera[0].GetComponent<Camera>().transform.forward; //Le digo que apunte hacia ella.
        }
        }catch{
            
        }
    }
}
