using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se utiliza para cambiar de camara.
public class ChangeCameraControl : MonoBehaviour
{
    public GameObject [] _array_Camera; //Array de camaras.
    // Start is called before the first frame update
    void Start()
    {
        //Activo la camara principal.
        _array_Camera[0].gameObject.SetActive(true);
        _array_Camera[1].gameObject.SetActive(false);
        _array_Camera[2].gameObject.SetActive(false);
    }

    public void Set_Camera(int index){ //cambia la camara activa
        _array_Camera[index].gameObject.SetActive(true);
        for(int i = 0; i < _array_Camera.Length; i++){
            if(i != index){
                _array_Camera[i].gameObject.SetActive(false);
            }
        }
    }
}
