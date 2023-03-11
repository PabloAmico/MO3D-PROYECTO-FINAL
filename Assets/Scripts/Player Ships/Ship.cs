using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : Unit
{
    private NavMeshAgent _agent;    

    private void Awake()
    {

        this._agent = GetComponent<NavMeshAgent>(); //Obtengo el agente del objeto.
    }

    public bool Get_Selected()
    {
        return _selected;
    }

    //Metodo para mover la nave.
    public override void OnMove(Vector3 WorldPos)
    {
        this._agent.isStopped = false;
        
        this._agent.Resume();
        this._agent.SetDestination(WorldPos);   //Le asigno un punto del mapa al cual moverse.
    }


//Metodo para detener la nave
    public override void OnStop()
    {
         this._agent.isStopped = true;
    }

    public override void Init()
    {
        if(gameObject.GetComponent<RecollectShip>() == null){
        OnMove(_create_Troops._position_Spawn.transform.position);
        }
       
    }

   
//Metodo para asignar la velocidad de la nave.
    public void Speed_Agent(float speed)
    {
        _agent.speed = speed;
    }

//Metodo para asignar la aceleracion de la nave.
    public void Acceleration_Agent(float acc)
    {
        _agent.acceleration = acc;
    }


//Metodo para asignar la velocidad de giro de la nave.
    public void AngularSpeed_Agent(float ang_speed)
    {
        _agent.angularSpeed = ang_speed;
    }
}
