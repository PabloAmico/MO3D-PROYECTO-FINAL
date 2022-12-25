using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMeteorite : MonoBehaviour
{
    // Start is called before the first frame update

    public Meteorite _meteorite;
    private Plane _plane;
    public float _time_Create = 0f; //tiempo de creacion de un meteorito
    public float _time_Current_Create = 0f;
    private Vector3 _positionRandom;
    void Start()
    {
        
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
            Random_Fall();
            Meteorite Meteorite_Instantiate = Instantiate(_meteorite,new Vector3(_positionRandom.x + 30f, transform.position.y, _positionRandom.z + 10f),Quaternion.identity);
            
            Meteorite_Instantiate._direction = _positionRandom;
        }
    }

    private void Random_Fall()
    {
        float X = 130;
        X = Random.Range(-1f, 1f) * X;
        float Y = 130;
        Y = Random.Range(-1f, 1f) * Y;
        _positionRandom = new Vector3(X, 0, Y);
    }
}
