using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Troops : MonoBehaviour
{

    public Ship _basic_Ship;
    public Ship _missile_Ship;
    public Ship _laser_Ship;
    public GameObject _position_Spawn;
    public int _cost_Basic = 75;
    public int _cost_Missile = 300;
    public int _cost_Laser = 150;
    private Money _money;
    public Message_Control _message;

    // Start is called before the first frame update
    void Start()
    {
        _money = GetComponent<Money>();
        _message = FindObjectOfType<Message_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instance_Basic_Ship()
    {
        if (_money.Get_Money() >= _cost_Basic)
        {
            Instantiate(_basic_Ship, transform.position, Quaternion.identity);
            _money.Less_Money(75);
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $75");
        }
    }

    public void Instance_Missile_Ship()
    {
        if (_money.Get_Money() >= _cost_Missile)
        {
            Instantiate(_missile_Ship, transform.position, Quaternion.identity);
            _money.Less_Money(300);
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $300");
        }
    }
    public void Instance_Laser_Ship()
    {
        if (_money.Get_Money() >= _cost_Laser)
        {
            Instantiate(_laser_Ship, transform.position, Quaternion.identity);
            _money.Less_Money(150);
        }
        else
        {
            _message.Show_Message("No hay dinero. Costo de tropa $150");
        }
    }
}
