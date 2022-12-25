using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectShip : StatsUnits
{
    public bool _is_Big;
    public bool _contact = false;

    public int _collect_Money = 0;
 
    private Money _money_Text;

    public float _timeRecollect;
    private float _timePaid;
    private RockOfMoney _objective_Recollect = null;

    private bool _destiny = false;

    public PoolRockOfMoney _pool_Money;

    //GUI
    public Image _circle_Fill;

    public Image _icon;

    public bool _sync = false;

    //private LaserRecollect _laser;
   
     protected override void Init()
    {
        //_laser = _ship.GetComponentInChildren<LaserRecollect>();
      
        //_money_Text = GameObject.FindGameObjectWithTag("HangarPlayer");
        _money_Text = FindObjectOfType<Money>();
        
        _pool_Money = FindObjectOfType<PoolRockOfMoney>();
      
        _timePaid = 0; 

        _circle_Fill.enabled = false;
        _icon.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!_destiny){
            Set_Destiny();
        }else{
            if (_destiny && !_sync){
                
                _sync = _money_Text.GoSync();
                //print("Sincronizando...");
                _ship.OnStop();
            }
        }
        Move_Ship();
        if (_contact && _sync)
        {
            _circle_Fill.enabled = true;
            _icon.enabled = true;
            _ship.OnStop();
            //_timePaid += Time.deltaTime;
            _circle_Fill.fillAmount = _money_Text.Get_Current_Time_Paid() / _money_Text.Get_Time_Paid();
            if(_money_Text.Get_Current_Time_Paid() >= _money_Text.Get_Time_Paid())
            {
                print("ENTRE " + _sync + " " + _contact);
                Collect_Money(_collect_Money);
                //_timePaid = 0f;
            }
        }else{
            _circle_Fill.enabled = false;
            _icon.enabled = false;
        }
    }

    

    public void Collect_Money(int Number)
    {
        if(_objective_Recollect != null){
            if (_objective_Recollect.Get_Current_Money() >= Number)
            {
                
                _money_Text.Add_New_Money(Number);
                _objective_Recollect.GetComponent<RockOfMoney>().Extract_Money(Number);
                print("RECOLECTE FULL");
            }
            else
            {
                _money_Text.Add_New_Money(_objective_Recollect.Get_Current_Money());
                _objective_Recollect.GetComponent<RockOfMoney>().Extract_Money(_objective_Recollect.Get_Current_Money());
                print("RECOLECTE UNA PARTE");
            }
            if(_objective_Recollect.Get_Current_Money() < 0)
            {
                print("ME MORI");
                _contact = false;
                Destroy(_objective_Recollect, 0.5f);
                _objective_Recollect = null;
                _destiny = false;
            }
        }
    }

   

    public void Set_Recollect(RockOfMoney recollect)
    {
        _objective_Recollect= recollect;
        print(recollect);
        if(recollect == null){
            print("No hay destino");
            _destiny = false;
        }
    }

    public void Set_Destiny(){
        if(!_destiny){
            float distance = 10000f;
           //aplicar contains.
            foreach (RockOfMoney rock in _pool_Money.Get_Rocks())   //Recorro mi array de rocas.
            {
                //print("Recorriendo...");
                float aux_distance = Vector2.Distance(this.transform.position, rock.transform.position);    //Obtengo la distancia entre la roca y mi nave.
                if(aux_distance < distance){    //corroboro la distancia con la distancia que tengo guardada
                    _objective_Recollect = rock;    //Le asigno el objetivo.
                    _destiny = true;
                }
            }
            print("Nuevo destino " + _objective_Recollect.name);
        }
    }

    private void Move_Ship(){
        if(_objective_Recollect != null){
            print("En movimiento");
            _ship.OnMove(_objective_Recollect.transform.position);
        }
    }
}
