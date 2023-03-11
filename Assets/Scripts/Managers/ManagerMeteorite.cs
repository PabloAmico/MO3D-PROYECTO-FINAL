using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMeteorite : MonoBehaviour
{
    public Meteorite _meteorite;    //Prefab de meteoritos
    private Plane _plane;   //El suelo
    private float _time_Create = 6f; //tiempo de creacion de un meteorito
    public float _time_Current_Create = 0f; //tiempo actual, cuando iguala a _time_Create se crea un meterorito
    private Vector3 _positionRandom;    //Posicion random donde caera el meteorito.
    public GameObject _Cristals = null; //Contenedor de todas las rocas de dinero del juego.
    public List<GameObject> _position_Exclude;  //Posiciones en las cuales el meteorito no puede caer, hangares y rocas de dinero.
    void Start()
    {
        RockOfMoney[] Cristal_Aux = _Cristals.GetComponentsInChildren<RockOfMoney>();   //Obtengo las rocas de dinero instanciadas en el juego
        for(int i = 0; i < Cristal_Aux.Length;i++){
            _position_Exclude.Add(Cristal_Aux[i].gameObject);   //Y agrego su posicion al array de exclusion.
        }
    }

    void Update()
    {
        Create_Meteorite(); //Creo los meteoritos
    }

    private void Create_Meteorite()
    {
        _time_Current_Create += Time.deltaTime;
        if(_time_Current_Create > _time_Create) //Si el tiempo actual supero el tiempo maximo.
        {
            _time_Current_Create = 0f;
            Set_Fall(); //Llamo a este metodo para crear la posicion de caida del meteorito.
            //Una vez obtenida la posicion en donde va caer el meteorito, le sumo 30 en el eje de las "X" y 10 en el eje de las "Z", para la creacion y asi obtener una caida de lado y no recta.
            Meteorite Meteorite_Instantiate = Instantiate(_meteorite,new Vector3(_positionRandom.x + 30f, transform.position.y, _positionRandom.z + 10f),Quaternion.identity);
            Meteorite_Instantiate._direction = _positionRandom; //Le asigno al meteorito el punto en el cual tiene que caer.
        }
    }


//Metodo que se utiliza en ManagerGameControl para cambiar el tiempo de creacion de los meteoritos.
    public void Set_Time(float NewTime){
        _time_Create = NewTime;
    }

//Metodo para obtener el tiempo de creacion.
    public float Get_Time_Create(){
        return _time_Create;
    }

//Metodo que contiene otros dos metodos dentro. Esto se utiliza para evitar caer sobre los puntos de exclusion.
    private void Set_Fall(){
        Random_Fall();
        Adjust_Fall();
    }

//Metodo para obtener un punto aleatorio de caida.
    private void Random_Fall()
    {

        float X = 130;
        X = Random.Range(-1f, 1f) * X;
        float Y = 130;
        Y = Random.Range(-1f, 1f) * Y;
        
        _positionRandom = new Vector3(X, 0, Y);
    }

//Metodo para ajustar la posicion de caida obtenida con Random_Fall y asi evitar los puntos de exclusion.
    private void Adjust_Fall(){
        foreach(GameObject obj in _position_Exclude){
            if(obj == null){    //Si el objeto ya no existe lo remuevo de la lista.
                _position_Exclude.Remove(obj);
            }
        }
        int It = 0; //Creo un iterador en 0.
        while(It < _position_Exclude.Count) //Mientras el iterador sea menor al punto de exclusion
    
        //si el numero al azar coincide con el box collider de algun objeto de la lista de exclusion
        if(_position_Exclude[It].GetComponent<BoxCollider>().bounds.Contains(_positionRandom)){ 
            It = 0; //Reinicio el iterador
            Random_Fall();  //Y vuelvo a tirar una posicion aleatoria
        }else{
            It++;   //sino sumo 1 al iterador.
        }
        
    }

//Metodo para remover un objeto de la lista de exclusion.
    public void Eliminate_Object(GameObject elim){
        _position_Exclude.Remove(elim);
    }
}
