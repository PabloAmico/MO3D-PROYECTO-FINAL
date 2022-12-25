using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOfMoney : MonoBehaviour
{
    public int _money;
    private int _current_Money;
    public bool _is_Big;

    private RecollectShip _ship = null;

    private PoolRockOfMoney _pool;
    // Start is called before the first frame update
    void Start()
    {
        _current_Money = _money;
        _pool = FindObjectOfType<PoolRockOfMoney>();
        _pool.Load_Rock(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(_money <= 0){
            Destroy(gameObject, 0.1f);
            _ship.Set_Recollect(null);
            _ship._sync = false;
            _ship._contact = true;
            _pool.Eliminate_Rocks(this);
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
