using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Texto que se muestra cuando una nave recibe daño.
public class TextHit : MonoBehaviour
{
    public bool _move;
    public float _max_Height = 0f;  //Altura maxima a la que tiene que moverse.
    public int _damage = 0; //Numero que tiene que mostrar.
    public CanvasHit _canvasParent;

    void Update()
    {
        On_Move();
    }

//Metodo para mostrar el daño recibido.
    public void Set_Text(int text, string name, CanvasHit parent)
    {
        _canvasParent = parent;
        _damage += text;    //Si recibo daño lo muestro, si recibo varios a la ves los sumo.
        gameObject.GetComponent<Text>().text = _damage.ToString();  //Lo convierto a texto.
    }

//Metodo para mover el texto hacia arriba.
    private void On_Move()
    {
        if( _move)
        {
            //Obtiene la posicion en x y z del objeto y aumenta en 1 en el eje de las y.
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            transform.rotation = gameObject.GetComponentInParent<Transform>().rotation; //tiene la rotacion del objeto
            //Si supera la altura maxima se destruye.
            if(transform.position.y > _max_Height)
            {
                _canvasParent.Destroy_Text();
                Destroy(gameObject,0.5f);
            }
        }
    }
}
