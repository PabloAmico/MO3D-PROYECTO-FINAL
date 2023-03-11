using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUnits : MonoBehaviour
{
    public int _points_Attack;  //Puntos de ataque
    public int _points_Life;    //Puntos de vida
    //public int _points_Percentage_Critical; //Variable no utilizada en este proyecto
    public float _points_Speed; //Velocidad
    public float _points_Acceleration;  //Aceleracion
    public float _points_AngularSpeed;  //Velocidad de giro
    public float _cooldown_Attack;  //Tiempo de ataque
    protected float _cooldown_Current;  //Tiempo actual de ataque
    public int _points_Life_Max = 0;    //puntos maximos de vida.
    public bool _is_Dead = false;   //Variable que se vuelve True cuando la nave muere.
    public Ship _ship = null;   //Clase ship que se obtiene de la nave.
    public bool _zone_Rocket = false;   //Variable para saber si la nave se encuentra en la zona del cohete.

    public StatsUnits _unit_Objective;  //Unidad objetivo.
    public List<StatsUnits> _units_InZone = new List<StatsUnits>(); //Lista de unidades en la zona
    public CanvasHit _canvas_Hit;   //Canvas que muestra el daño recibido

    private SFX_Radio _radio;   //Radio para reproducir el audio de las tropas.

    protected ShowSelectShip _show_Select = null;   //Clase que muestra cuando la nave del jugador es seleccionada.
    void Start()
    {
        //Asignolas variables y busco los objetos tanto en la nave como en la escena.
        _ship = GetComponent<Ship>();
        _ship.Speed_Agent(_points_Speed);
        _ship.Acceleration_Agent(_points_Acceleration);
        _ship.AngularSpeed_Agent(_points_AngularSpeed);
        _cooldown_Current = _cooldown_Attack;
        _radio = FindObjectOfType<SFX_Radio>();
        _canvas_Hit = GetComponentInChildren<CanvasHit>();
        _show_Select = GetComponentInChildren<ShowSelectShip>();
        Init();
    }

//Metodo para eliminar el objetivo que tiene asignada la nave.
    public void Eliminate_Objective(StatsUnits Objective)
    {
        if(_unit_Objective == Objective)    //Si el objetivo es igual al que tengo asignado
        {
            _unit_Objective = null; //Lo vuelvo nulo.
        }
         _units_InZone.Remove(Objective);   //Remuevo el objetivo de la lista de unidades cercanas.
        if(_units_InZone.Count > 0) //Si tengo mas unidades cercanas
        {
            _unit_Objective = _units_InZone[0]; //Asigno a la que sigue en la lista como objetivo.
        }
        else
        {
            _unit_Objective= null;
        }
    }

    protected virtual void Init()
    {

    }
    

    //Muestra el daño recibido .
    private void Set_Hit(int Damage)
    {
       
        _canvas_Hit.Create_Text(Damage);
    }
   

   //Setea el daño de la nave.
    public void Set_Damage(int Damage)
    {
        _points_Life -= Damage;
        Set_Hit(Damage);
    }

}
