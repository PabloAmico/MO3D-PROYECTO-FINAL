using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Clase para mover la camara.
public class CameraControl : MonoBehaviour
{
    ChangeCameraControl _change_Cam;    //Clase que sirve para cambiar de camaras.
    public float _border = 5f;  //Constante para mover la camara ucuando me acerco al borde.
    private Vector2 _screen_Border; //Borde de la pantalla
    private float _mousePositionX;  //Posicion del mouse.
    public float _move_Speed = 20f; //Velocidad de movimiento de la camara.
    private Vector3 _move;  //Vector de movimiento

    public Vector2 _move_Limits;    //Limites de movimiento (punto hasta el cual se puede mover la camara).

    public float _zoom_Speed = 20f; //Velocidad del zoom

    public float MinY = 20f;    //Altura minima de la camara (zoom).
    public float MaxY = 120f;   //Altura maxima de la camara (zoom).

    Rocket _rocket; //Necesito el cohete para poder mover la camara rapido hacia el.


    void Start()
    {
        _change_Cam = FindObjectOfType<ChangeCameraControl>();
        _change_Cam.Set_Camera(0);  //seteo esta camara como la principal.
        _rocket = FindObjectOfType<Rocket>();
       
    }

 
    void Update()
    {

        Vector3 Pos = transform.position;

        //Si presiono una de las teclas W,A,S y D o me acerco a un borde la camara se mueve.

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - _border)
        {
            Pos.z += _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= _border)
        {
            Pos.z -= _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - _border)
        {
            Pos.x += _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= _border)
        {
            Pos.x -= _move_Speed * Time.deltaTime;
        }

       


        //Para el zoom utilizo la rueda del mouse
        float Zoom = Input.GetAxis("Mouse ScrollWheel");

        Pos.y -= Zoom * _zoom_Speed * 100f * Time.deltaTime; 

        //Asigno los cambios a las variables.
        Pos.x = Mathf.Clamp(Pos.x, -_move_Limits.x, _move_Limits.x);
        Pos.y = Mathf.Clamp(Pos.y, MinY,MaxY);
        Pos.z = Mathf.Clamp(Pos.z, -_move_Limits.y, _move_Limits.y);

        //Asigno estos cambios a la posicion de la camara.
        transform.position = Pos;

        //Si presiono la tecla espacio muevo la camara instantaneamente a la posicion del cohete.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(_rocket.transform.position.x, Pos.y, _rocket.transform.position.z - 5f);
        }
    }
}
