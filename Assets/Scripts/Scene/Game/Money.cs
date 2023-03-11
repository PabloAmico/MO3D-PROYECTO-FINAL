using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private int _money = 300;   //Dinero inicial.
    private int _normal_Money = 50; //Dinero que se agrega normalmente, se recolecte o no dinero.
    private int _new_Money = 50;    //Dinero que se agrega en cada tiempo de pago.
    private float _time_Paid = 2f;  //Tiempo de pago
    private float _current_time_Paid;   //Tiempo actual de pago.
    
    private bool _sync = false; //Sincronizacion con la nave de recoleccion
    private float _time_Sync = 0.1f;    //Teimpo de espera para sincronizar
    public Text _moneyText; //Texto que muestra el dinero en pantalla.

    
    // Start is called before the first frame update
    void Start()
    {
        _current_time_Paid = 0f;
        _moneyText.text ="$ "+ _money.ToString();   //Muestro el dinero en pantalla
    }

    void Update()
    {
         Restart_Time_Current();
        _current_time_Paid += Time.deltaTime;
        if(_current_time_Paid >= _time_Paid)    //Si el tiempo actual es mayort al tiempo de pago.
        {
            _sync = true;   //Sincronizo con la nave
            Paid_Money();   //Y sumo el dinero.
        }

        Sync();
    }

//Metodo que reinicia el tiempo actual.
    private void Restart_Time_Current(){

        if(_current_time_Paid >= _time_Paid)
        _current_time_Paid = 0f;
    }

//Metodo que agrega dinero en cada tiempo de pago.
    public void Add_New_Money(int add)
    {
        _new_Money += add;
    }


//Metodo para extraer dinero (se utiliza cuando se instancian las naves del jugador).
    public void Less_Money(int less)
    {
        _money -= less;
        _moneyText.text = "$ " + _money.ToString();
    }


//Metodo que se utiliza para cargar dinero al total.
    private void Paid_Money()
    {
        _money += _new_Money + _normal_Money;   //Se suma al total, el dinero que recolecto la nave, mas el que se suma normalmente
        _new_Money = 0; //pongo en cero el dinero que recolecto la nave
        _moneyText.text = "$ " + _money.ToString();
    }


//Metodo para devolver el dinero.
    public int Get_Money()
    {
        return _money;
    }


//Metodo para sincronizar con la nave de recoleccion
    private void Sync(){
        if(_sync){
            _time_Sync -= Time.deltaTime;
            if(_time_Sync <= 0){
                _sync = false;
                _time_Sync = 0.1f;
            }
        }
    }

    public float Get_Time_Paid(){
        return _time_Paid;
    }

    public float Get_Current_Time_Paid(){
        return _current_time_Paid;
    }

    public bool GoSync(){
        return _sync;
    }
}
