using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_IA_Spawn : MonoBehaviour
{

    private Hangar_Enemy[] _hangars;
    public Ship _ship_Enemy;
    public float _time_Spawn_Enemy;
    private float _time_Current_Spawn;
    private int _current_Hangar = 0;
    ManagerUnit _unit;
    // Start is called before the first frame update
    void Start()
    {
        _hangars = GetComponentsInChildren<Hangar_Enemy>();
        _unit = FindObjectOfType<ManagerUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (_unit._units_Total.Count < _unit._max_Units)
        {
            bool Attacking_hangar = false;
            _time_Current_Spawn += Time.deltaTime;
            if (_time_Current_Spawn >= _time_Spawn_Enemy)
            {
                _time_Current_Spawn = -2;
                foreach (Hangar_Enemy _hangar in _hangars)
                {
                    if (_hangar._is_Attacked)
                    {
                        Instantiate(_ship_Enemy, _hangars[_current_Hangar].transform.position, Quaternion.identity);

                        Attacking_hangar = true;
                    }
                }

                if (!Attacking_hangar)
                {
                    Instantiate(_ship_Enemy, _hangars[_current_Hangar].transform.position, Quaternion.identity);
                }
                _current_Hangar++;
                if (_current_Hangar >= _hangars.Length)
                {
                    _current_Hangar = 0;
                }
            }
        }
    }
}
