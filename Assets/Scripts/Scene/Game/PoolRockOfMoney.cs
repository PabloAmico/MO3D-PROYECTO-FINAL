using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRockOfMoney : MonoBehaviour
{
    public List<RockOfMoney> _rocks = new List<RockOfMoney>();
    public RockOfMoney _big_Rock = null;
    public RockOfMoney _small_Rock = null;
    private Vector3 _positionRandom;
    public List<GameObject> _position_Exclude;

    private float _pos_X;
    private float _pos_Z;

    private void Awake() {
        //Instance_Rocks();
    }
    // Start is called before the first frame update

  

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

    
    /*private void Instance_Rocks(){
        for(int i = 0; i < 30; i++){
            if(i < 20){
                Random_Instance();
                Adjust_position();
                RockOfMoney Bigrock = Instantiate(_big_Rock, _positionRandom, Quaternion.identity);
                Bigrock.gameObject.transform.SetParent(gameObject.transform.parent);
                Load_Rock(Bigrock);
                RockOfMoney Smallrock = Instantiate(_small_Rock, _positionRandom, Quaternion.identity);
                Smallrock.gameObject.transform.SetParent(gameObject.transform.parent);
                Load_Rock(Smallrock);
            }else{
                Random_Instance();
                Adjust_position();
                RockOfMoney Smallrock = Instantiate(_small_Rock, _positionRandom, Quaternion.identity);
                Smallrock.gameObject.transform.SetParent(gameObject.transform.parent);
            }
        }
        
    }*/

    /*private void Random_Instance(){
        _pos_X = 130;
        _pos_X = Random.Range(-1f, 1f) * _pos_X;
        _pos_Z = 130;
        _pos_Z = Random.Range(-1f, 1f) * _pos_Z;
        
        _positionRandom = new Vector3(_pos_X, 0, _pos_Z);
    }

    private void Adjust_position(){
        foreach(GameObject obj in _position_Exclude){
            if(obj == null){
                _position_Exclude.Remove(obj);
            }
        }
        int It = 0;
        while(It < _position_Exclude.Count)
    
        if(_position_Exclude[It].GetComponent<BoxCollider>().bounds.Contains(_positionRandom)){
            It = 0;
            Random_Instance();
        }else{
            It++;
        }
        
    }

    private void Adjust_Position_Rock(){
        
    }*/
}
