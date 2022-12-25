using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private int _money = 1000;
    private int _normal_Money = 50; //Dinero que se agrega normalmente, se recolecte o no dinero.
    private int _new_Money = 50;
    private float _time_Paid = 2f;
    private float _current_time_Paid;
    
    private bool _sync = false;
    private float _time_Sync = 0.1f;
    public Text _moneyText;

    
    // Start is called before the first frame update
    void Start()
    {
        _current_time_Paid = 0f;
        _moneyText.text ="$ "+ _money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
         Restart_Time_Current();
        _current_time_Paid += Time.deltaTime;
        if(_current_time_Paid >= _time_Paid)
        {
            _sync = true;
            Paid_Money();
        }

        Sync();
    }
    

    private void LateUpdate() {
       
    }

    private void Restart_Time_Current(){

        if(_current_time_Paid >= _time_Paid)
        _current_time_Paid = 0f;
    }

    public void Add_New_Money(int add)
    {
        _new_Money += add;
    }

    public void Less_Money(int less)
    {
        _money -= less;
        _moneyText.text = "$ " + _money.ToString();
    }

    private void Paid_Money()
    {
        _money += _new_Money + _normal_Money;
        _new_Money = 0;
        _moneyText.text = "$ " + _money.ToString();
    }

    public int Get_Money()
    {
        return _money;
    }

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
