using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUnits : MonoBehaviour
{
    public int _points_Attack;
    public int _points_Life;
    public int _points_Percentage_Critical;
    public float _points_Speed;
    public float _points_Acceleration;
    public float _points_AngularSpeed;
    public float _cooldown_Attack;
    protected float _cooldown_Current;
    public StatsUnits _unit_Objective;
    public List<StatsUnits> _units_InZone = new List<StatsUnits>();
    public CanvasHit _canvas_Hit;

    
    public int _points_Life_Max = 0;

    public bool _is_Dead = false;

    public Ship _ship = null;

    public bool _zone_Rocket = false;

    private SFX_Radio _radio;
    void Start()
    {
        _ship = GetComponent<Ship>();
        _ship.Speed_Agent(_points_Speed);
        _ship.Acceleration_Agent(_points_Acceleration);
        _ship.AngularSpeed_Agent(_points_AngularSpeed);
        _cooldown_Current = _cooldown_Attack;
        _radio = FindObjectOfType<SFX_Radio>();
        _canvas_Hit = GetComponentInChildren<CanvasHit>();
        Init();
    }

    public void Eliminate_Objective(StatsUnits Objective)
    {
        if(_unit_Objective == Objective)
        {
            _unit_Objective = null;
        }
         _units_InZone.Remove(Objective);
        if(_units_InZone.Count > 0)
        {
            _unit_Objective = _units_InZone[0];
        }
        else
        {
            _unit_Objective= null;
        }
    }

    void Update()
    {
        
    }

    protected virtual void Init()
    {

    }
    
    private void Set_Hit(int Damage)
    {
       
        _canvas_Hit.Create_Text(Damage);
    }
   
    public void Set_Damage(int Damage)
    {
        _points_Life -= Damage;
        Set_Hit(Damage);
    }

}
