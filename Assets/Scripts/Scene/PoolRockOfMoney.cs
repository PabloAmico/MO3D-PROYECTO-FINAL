using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRockOfMoney : MonoBehaviour
{
    public List<RockOfMoney> _rocks = new List<RockOfMoney>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load_Rock(RockOfMoney Rock){
        _rocks.Add(Rock);
        //print("Roca numero " + _rocks.Count);
    }

    public List<RockOfMoney> Get_Rocks(){
        return _rocks;
    }

    public void Eliminate_Rocks(RockOfMoney Rock){
        _rocks.Remove(Rock);
    }

 
}
