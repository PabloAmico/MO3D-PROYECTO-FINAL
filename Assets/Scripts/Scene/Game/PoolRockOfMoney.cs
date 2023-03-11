using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRockOfMoney : MonoBehaviour
{
    public List<RockOfMoney> _rocks = new List<RockOfMoney>();  //Lista de rocas.

//Metodo para cargar una roca al pool
    public void Load_Rock(RockOfMoney Rock){
        _rocks.Add(Rock);
    }

//Metodo para obtener la lista de rocas que contiene el pool.
    public List<RockOfMoney> Get_Rocks(){
        return _rocks;
    }

//Metodo para eliminar una roca del pool
    public void Eliminate_Rocks(RockOfMoney Rock){
        _rocks.Remove(Rock);
    }

    
 
}
