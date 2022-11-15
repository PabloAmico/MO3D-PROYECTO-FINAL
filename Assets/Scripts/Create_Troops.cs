using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Troops : MonoBehaviour
{

    public Ship _basic_Ship;
    public Ship _missile_Ship;
    public Ship _laser_Ship;
    public Vector3 _position_Spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instance_Basic_Ship()
    {
        Instantiate(_basic_Ship,_position_Spawn,Quaternion.identity);
    }

    public void Instance_Missile_Ship()
    {
        Instantiate(_missile_Ship,_position_Spawn,Quaternion.identity);
    }
    public void Instance_Laser_Ship()
    {
        Instantiate(_laser_Ship, _position_Spawn, Quaternion.identity);
    }
}
