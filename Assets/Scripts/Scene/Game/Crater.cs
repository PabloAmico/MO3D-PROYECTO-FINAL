using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se activa cuando un meteorito toca la superficie de la tierra. Aplica daño a las naves.
public class Crater : MonoBehaviour
{
    private float _time_enabled = 0.2f; //Tiempo que se encuentra activo el daño del crater.
    BoxCollider _collider = null;
    void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider>(); 
        gameObject.GetComponent<MeshRenderer>().enabled = false;    //Desactivo la malla para que sea invisible.
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    void Update()
    {
        _time_enabled -= Time.deltaTime;    //Resto el tiempo 
        if(_time_enabled <= 0)  //Si es menor o igual a cero.
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true; //Habilito la malla
            _collider.enabled = false;  //Y desactivo el collider.
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "Ship Enemy"){
            other.GetComponent<StatsUnits>()._points_Life -= 100;   //Si coliciona con alguna nave le resta 100 puntos de vida.
        }
    }
}
