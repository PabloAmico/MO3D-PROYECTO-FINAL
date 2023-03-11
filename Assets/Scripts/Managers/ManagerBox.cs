using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBox : MonoBehaviour
{
    public Selection_Box box;
    private bool _selecting = false;

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))    //Si hago click izquierdo.
        {
            this._selecting = true; //Estoy seleccionando
            box.BeginBox(Input.mousePosition);  //Le digo a la caja de seleccion el punto inicial.
        }

        if (_selecting) //si estoy seleccionando.
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) //Si muevo el mouse haciendo click izquierdo.
            {
                box.DragClick(Input.mousePosition); //Paso la posicion actual del mouse.
            }
        }


        if (Input.GetMouseButtonUp(0))  //Si dejamos de presionar el click izquierdo
        {

            box.EndClick(); //le aviso a la caja de seleccion
            this._selecting = false;
        }
    }
}

