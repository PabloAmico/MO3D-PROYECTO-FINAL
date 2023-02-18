using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMeteorite : MonoBehaviour
{
    // Start is called before the first frame update

    public Meteorite _meteorite;
    private Plane _plane;
    private float _time_Create = 6f; //tiempo de creacion de un meteorito
    public float _time_Current_Create = 0f;
    private Vector3 _positionRandom;
    public GameObject _Cristals = null;
    public List<GameObject> _position_Exclude;
    void Start()
    {
        RockOfMoney[] Cristal_Aux = _Cristals.GetComponentsInChildren<RockOfMoney>();
        for(int i = 0; i < Cristal_Aux.Length;i++){
            _position_Exclude.Add(Cristal_Aux[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Create_Meteorite();
    }

    private void Create_Meteorite()
    {
        _time_Current_Create += Time.deltaTime;
        if(_time_Current_Create > _time_Create)
        {
            
            _time_Current_Create = 0f;
            Set_Fall();
            Meteorite Meteorite_Instantiate = Instantiate(_meteorite,new Vector3(_positionRandom.x + 30f, transform.position.y, _positionRandom.z + 10f),Quaternion.identity);
            
            Meteorite_Instantiate._direction = _positionRandom;
        }
    }


    public void Set_Time(float NewTime){
        _time_Create = NewTime;
    }

    public float Get_Time_Create(){
        return _time_Create;
    }
    private void Set_Fall(){
        Random_Fall();
        Adjust_Fall();
    }

    private void Random_Fall()
    {

        float X = 130;
        X = Random.Range(-1f, 1f) * X;
        float Y = 130;
        Y = Random.Range(-1f, 1f) * Y;
        
        _positionRandom = new Vector3(X, 0, Y);
    }

    private void Adjust_Fall(){
        foreach(GameObject obj in _position_Exclude){
            if(obj == null){
                _position_Exclude.Remove(obj);
            }
        }
        int It = 0;
        while(It < _position_Exclude.Count)
    
        if(_position_Exclude[It].GetComponent<BoxCollider>().bounds.Contains(_positionRandom)){
            It = 0;
            Random_Fall();
        }else{
            It++;
        }
        
    }

    public void Eliminate_Object(GameObject elim){
        _position_Exclude.Remove(elim);
    }
}
