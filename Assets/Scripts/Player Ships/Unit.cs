using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    protected bool _selected;   //True si la nave se encuentra seleccionada.
    public ManagerUnit _manager_Unit;
    public bool _select_Attack = false;
    protected Create_Troops _create_Troops;
    
    public bool Is_Selected
    {
        get 
        { 
            return this._selected; 
        
        }
        
        set 
        { 

            //mostrar icono de seleccion
            this._selected = value; 
        
        }
    }

    void Start()
    {
       

        this.Is_Selected = false;
        if (this.GetComponent<Faction>().Is_PlayerUnit)
        {
            _manager_Unit = GameObject.FindObjectOfType<ManagerUnit>();
            _manager_Unit._units.Add(this); //Agrego este objeto a la lista de unidades del ManagerUnits.
        }

        try{
            _manager_Unit._units_Total.Add(this);
        }catch{
        }
        _create_Troops = FindObjectOfType<Create_Troops>();
        Init();
   
    }

//Metodo para remover unidades del manager unit.
    public void RemoveShip(Unit unit)
    {
        _manager_Unit._units.Remove(unit);
        _manager_Unit._units_Selected.Remove(unit);
        _manager_Unit._units_Total.Remove(unit);
    }
   
//Metodos virtuales.
    public virtual void Init()
    {

    }

    public virtual void OnMove(Vector3 WorldPos)
    {

    }

    public virtual void OnStop()
    {

    }
}
