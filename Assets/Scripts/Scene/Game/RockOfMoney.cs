using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOfMoney : MonoBehaviour
{
    public int _money;  //Dinero maximo que tiene la roca.
    private int _current_Money;
    public bool _is_Big;    //Si la roca es grande.
    private bool _destroy = false;

    private RecollectShip _ship = null; //Nave de recoleccion

    private PoolRockOfMoney _pool;

    private ManagerMeteorite _meteorite;

    void Start()
    {
        
        _meteorite = FindObjectOfType<ManagerMeteorite>();
        _current_Money = _money;
        _pool = FindObjectOfType<PoolRockOfMoney>();
        _pool.Load_Rock(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(_money <= 0){    //Si dinero es menor o igual que cero se destruye.
           
            OnDestroy();
        }
    }

    private void OnDestroy(){
        if(!_destroy){  //Si destroy es falso
            _destroy = true;    //Se convierte en verdadero.
            try{
                _ship.Set_Recollect(null);  //Le paso a la nave que no tiene objetivo de recoleccion
                _ship._sync = false;    //Seteo en falso la sincronizacion
                _ship._contact = false; //Falso el contacto.
                _pool.Eliminate_Rocks(this);    //Elimino esta roca del pool.
                _meteorite.Eliminate_Object(this.gameObject);   //Elimino esta roca de los objetos que el meteorito debe excluir.
                Destroy(this.gameObject, 0.1f); //DEstruyo el objeto en 0.1f
            }catch{
                Destroy(this.gameObject);
            }
        }
    }

    public void Set_Ship(RecollectShip Ship){
        _ship = Ship;
    }


//Metodo para extraer dinero.
    public void Extract_Money(int extract)
    {
        _money -= extract;
    }

    public int Get_Current_Money()
    {
        return _current_Money;
    }
}
