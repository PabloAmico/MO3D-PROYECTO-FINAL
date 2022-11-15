using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : Unit
{
    private NavMeshAgent _agent;

    // Start is called before the first frame update

    private void Awake()
    {

        this._agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Get_Selected()
    {
        return _selected;
    }
    public override void OnMove(Vector3 WorldPos)
    {
        
      
        this._agent.isStopped = false;
        
        this._agent.Resume();
        this._agent.SetDestination(WorldPos);
        //print("ONMOVE! " + WorldPos);
    }

    public override void OnStop()
    {

         this._agent.isStopped = true;
    }

    public override void Init()
    {
       // this._agent = GetComponent<NavMeshAgent>();
       
    }

   

    public void Speed_Agent(float speed)
    {
        _agent.speed = speed;
    }

    public void Acceleration_Agent(float acc)
    {
        _agent.acceleration = acc;
    }

    public void AngularSpeed_Agent(float ang_speed)
    {
        _agent.angularSpeed = ang_speed;
    }

  
}
