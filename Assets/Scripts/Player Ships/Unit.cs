using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    //public Camera _sceneCamera;
    //private NavMeshAgent _agent;
    protected bool _selected;
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


    private void Awake()
    {
       // Init();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        //Debug.Log("Start");
        this.Is_Selected = false;
        //print("Creado");
        if (this.GetComponent<Faction>().Is_PlayerUnit)
        {
            _manager_Unit = GameObject.FindObjectOfType<ManagerUnit>();
            //print(_manager_Unit);
            _manager_Unit._units.Add(this);
        }

        try{
            _manager_Unit._units_Total.Add(this);
        }catch{
            //Debug.LogError(this);
           // print("Error en nave " + gameObject.name);
        }
        //_manager_Unit._units_Total.Add(this);
        _create_Troops = FindObjectOfType<Create_Troops>();
        //print(gameObject.name);
        Init();
   
    }

    public void RemoveShip(Unit unit)
    {
        _manager_Unit._units.Remove(unit);
        _manager_Unit._units_Selected.Remove(unit);
    }
   
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
