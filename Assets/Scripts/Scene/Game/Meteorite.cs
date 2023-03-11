using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Vector3 _direction;  //Direccion a la que tiene que ir el meteorito
    public float _velocity; //Velocidad de movimiento.
    public Crater _crater = null;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play(); //Activo el sistema de particulas.
    }

    void Update()
    {
        Move_Meteorite();
    }

//Metodo para mover el meteorito al lugar deseado.
    private void Move_Meteorite()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction, _velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject,0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))   //Si coliciona con el suelo
        {
            //Instancia un crater.
            Instantiate(_crater,transform.position,Quaternion.identity);
            OnDestroy();
        }
    }
}
