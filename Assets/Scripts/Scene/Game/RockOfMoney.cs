using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOfMoney : MonoBehaviour
{
    public int _money;
    private int _current_Money;
    public bool _is_Big;
    private bool _destroy = false;

    private RecollectShip _ship = null;

    private PoolRockOfMoney _pool;

    private ManagerMeteorite _meteorite;
    // Start is called before the first frame update
    void Start()
    {
        //_ship = FindObjectOfType<RecollectShip>();
        _meteorite = FindObjectOfType<ManagerMeteorite>();
        _current_Money = _money;
        _pool = FindObjectOfType<PoolRockOfMoney>();
        _pool.Load_Rock(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(_money <= 0){
           
            OnDestroy();
        }
    }

    private void OnDestroy(){
        if(!_destroy){
            _destroy = true;
            try{
                _ship.Set_Recollect(null);
                _ship._sync = false;
                _ship._contact = false;
                _pool.Eliminate_Rocks(this);
                _meteorite.Eliminate_Object(this.gameObject);
                Destroy(this.gameObject, 0.1f);
            }catch{
                //print("La roca que molesta es: " + this.gameObject.name);
                /*_ship.Set_Recollect(null);
                _ship._sync = false;
                _ship._contact = false;
                _pool.Eliminate_Rocks(this);
                _meteorite.Eliminate_Object(this.gameObject);*/
                Destroy(this.gameObject);
            }
        }
    }

    public void Set_Ship(RecollectShip Ship){
        _ship = Ship;
    }

    public void Extract_Money(int extract)
    {
        _money -= extract;
    }

    public int Get_Current_Money()
    {
        return _current_Money;
    }
}
