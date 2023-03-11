using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase que crea las tropas del jugador
public class Create_Troops : MonoBehaviour
{

    public Ship _basic_Ship;    //prefab nave basica.
    public Ship _missile_Ship;  //Prefab nave de misil.
    public Ship _laser_Ship;    //Prefab nave laser.
    public GameObject _position_Spawn;  //Posicion donde se instancian las naves.
    public int _cost_Basic = 75;    //Costo de la nave basica.
    public int _cost_Missile = 300; //Costo de la nave de misil.
    public int _cost_Laser = 150;   //costo de la nave laser.
    private Money _money;   
    public Message_Control _message;

    void Start()
    {
        _money = GetComponent<Money>();
        _message = FindObjectOfType<Message_Control>();
    }


//Metodo para instanciar Las naves basicas.
    public void Instance_Basic_Ship()
    {
        if (_money.Get_Money() >= _cost_Basic)  //Si tengo dinero.
        {
            Instantiate(_basic_Ship, transform.position, Quaternion.identity);  //Instancio la nave.
            _money.Less_Money(75);  //Y resto el dinero.
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $75");     //Sino, muestro un mensaje de que falta dinero para comprarla.
        }
    }

//Metodo para instanciar las naves de misiles.
    public void Instance_Missile_Ship()
    {
        if (_money.Get_Money() >= _cost_Missile)    //Si tengo dinero.
        {
            Instantiate(_missile_Ship, transform.position, Quaternion.identity);    //Instancio la nave.
            _money.Less_Money(300); //Y resto el dinero.
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $300");    //Sino, muestro un mensaje de que falta dinero para comprarla.
        }
    }

//Metodo para instanciar las naves laser.
    public void Instance_Laser_Ship()
    {
        if (_money.Get_Money() >= _cost_Laser)  //Si tengo dinero.
        {
            Instantiate(_laser_Ship, transform.position, Quaternion.identity);  //Instancio la nave.
            _money.Less_Money(150); //Y resto el dinero.
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $150");    //Sino, muestro un mensaje de que falta dinero para comprarla.
        }
    }
}
