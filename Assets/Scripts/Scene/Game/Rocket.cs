using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    MoveRocket _move = null;
    ChangeCameraControl _change_Cam;
    public float _time_Complete;    //Tiempo total para que este completa la nave.
    public float _time_current; //Tiempo actual de armado de la nave.
    private int _percentage = 1;    //Porcentaje de armado del cohete.
    private int _percentage_Prev;   //Porcentaje anterior. Esto lo utilizo para armar o desarmar dependiendo del porcentaje de progreso.
    public int _reset_Damage;   //Reinicia el daño recibido.
    private int _damage;    //Daño recibido por las naves enemigas.
    public GameObject[] _Rocket_Parts;  //Array con las partes del cohete.
    Canvas _canvas;
    public Image _fill_Image;   //Imagen del porcentaje de progreso.
    private Text _text;
    ParticleSystem[] _particles;    //Array con el sistema de particulas.
    public List<GameObject> _enemy_List = new List<GameObject>();   //Lista de enemigos que se encuentran disparando al cohete.

    ManagerGameControl _game_Control = null;

    AudioSource [] _sound;  //Sonidos cuando despega y cuando se esta destruyendo.

    private bool _first_Time_100 = true;   //booleano que sirve para saber si es la primera vez que se ingresa al loop una vez la nave completa al 100%
                                            //se utiliza para cambiar de camara.
    void Start()
    {

        //busqueda de los componentes.
        _sound = GetComponents<AudioSource>();
        _game_Control = FindObjectOfType<ManagerGameControl>();
        _move = GetComponent<MoveRocket>();
        _change_Cam = FindObjectOfType<ChangeCameraControl>();
        _canvas = GetComponentInChildren<Canvas>();
        _text = GetComponentInChildren<Text>();
        
        
        _move.enabled = false;  //La nave se encuentra quieta.
        _percentage = 0;
        _percentage_Prev = _percentage; 
        
        foreach (GameObject part in _Rocket_Parts)  //Desactivo todas las partes del cohete
        {
            part.SetActive(false);
        }
        _Rocket_Parts[0].SetActive(true);   //Y activo solo la primera.
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
        {
            particle.Stop();    //Detengo todas las particulas.
        }
    }

    void Update()
    {
        if (_enemy_List.Count > 0)  //si la lista no esta vacia
        {
            if (_enemy_List[0] == null) //y se elimino de escena el enemigo
            {
                _enemy_List.RemoveAt(0);    //remuevo el elemento para no apuntar a un objeto nulo
            }
        }
        Percentage();
        UI_Look_Camera();
    }

    void Percentage()   //reveer sistema de porcentaje.
    {
        _time_current += Time.deltaTime;
       
        if(_time_current >= _time_Complete / 100)   //Si el tiempo actual es 1% del total
        {
            _percentage_Prev = _percentage; //le paso el porcentaje a prev
            _percentage++;  //Sumo el porcentaje en 1.
            _time_current = 0;  //Reinicio el tiempo
            
        }
       

       //Por cada 20% voy sumando partes. 
        if( _percentage > 0  && _percentage < 20)
        {
            if ( _percentage_Prev > 20 || _percentage_Prev ==0)
            {
              
                Update_Parts_Rocket(0);
            }
        }
        else
        {
            if(_percentage >= 20 && _percentage < 40)
            {
                if (_percentage_Prev >= 40 || _percentage_Prev < 20)
                {
                   
                    Update_Parts_Rocket(1);
                        _particles[1].Stop();
                        _particles[0].Stop();
                }
            }
            else
            {
                if(_percentage >= 40 && _percentage < 60)
                {
                    if ( _percentage_Prev >= 60 || _percentage_Prev <40)
                    {
                        
                        Update_Parts_Rocket(2);
                        _particles[1].Stop();
                        _particles[0].Stop();
                    }
                   
                }
                else
                {
                    if(_percentage >= 60 && _percentage < 80)
                    {
                        if ( _percentage_Prev >= 80 || _percentage_Prev < 60)
                        {
                            
                            Update_Parts_Rocket(3);
                            _particles[1].Play();   //Activo las particulas del humo
                             _particles[0].Stop();
                        }
                        
                    }
                    else
                    {
                        if(_percentage >= 80 && _percentage < 100)
                        {
                            if ( _percentage_Prev >= 100 || _percentage_Prev < 80)
                            {
                                
                                Update_Parts_Rocket(4);
                                _particles[1].Play();   //Activo las particulas del humo
                                _particles[0].Play();   //Activo las particulas del fuego.
                            }
                        }
                        else
                        {
                            if(_percentage >= 100)
                            {
                                if(_first_Time_100){    //Si es la primera vez que accede al 100% (evito que ingrese varias veces.)
                                    _move.enabled = true;   //Se comienza a mover
                                    _change_Cam.Set_Camera(1);  //Cambio de camara.
                                    _first_Time_100 = false;   
                                    _move._move_Rocket = true;  //Muevo el cohete.
                                    Remove_Bar_Percentage();    //Elimino la barra de porcentaje
                                    Sound_Rocket(); //habilito los sonidos de despegue.
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void Sound_Rocket(){
        _sound[0].Play();
    }

    public void Sound_Explosion(){
        _sound[1].Play();
    }

    public void Remove_Bar_Percentage(){
        _canvas.enabled = false;
    }


//Metodo para habilitar y deshabilitar las partes del cohete.
    private void Update_Parts_Rocket(int part){
        for(int i = 0; i < _Rocket_Parts.Length; i++){
            if(i <= part){
                _Rocket_Parts[i].SetActive(true);
            }else{
                _Rocket_Parts[i].SetActive(false);
            }
        }
    }

    public void Particles_Destroy(){
        _particles[2].Play();
    }

    public int Get_Percentage(){
        return _percentage;
    }

//Metodo para setear el daño recibido por las naves.
    public void Set_Damage(int Damage)
    {
        _damage += Damage;  //Voy sumando el daño recibido.
        
        if(_damage >= _reset_Damage)    //Si es mayor al daño maximo
        {
           
            _damage -= _reset_Damage; //Reseteo el damage y le asigno lo que sobra (por si el da�o es mayor)
            _percentage_Prev = _percentage;
            _percentage -= 1;
            _game_Control.Check_Destroy_Rocket();   //cada vez que pierdo 1% chequeo el estado del cohete. si es menor que 0 pasa a la escena de destruccion
           
        }
    }


//Metodo para hacer que la barra de porcentaje del cohete mire siempre hacia la camara.
    private void UI_Look_Camera()
    {
        if(_change_Cam._array_Camera[0].activeSelf){    //Si la camara principal esta activa.
             _canvas.transform.forward = _change_Cam._array_Camera[0].GetComponent<Camera>().transform.forward; //Apunto el canvas hacia ella.
        }
      
        _fill_Image.fillAmount = (float)_percentage / 100;  //Relleno la imagen.
        _text.text = _percentage.ToString() + "%";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullets") || other.gameObject.CompareTag("Missile"))
        {
            if (other.gameObject.GetComponent<BaseBullets>()._ship_Shooter.gameObject != null)
            {
                if (other.gameObject.GetComponent<BaseBullets>()._ship_Shooter.gameObject.CompareTag("Ship Enemy"))
                {
                    if (!_enemy_List.Contains(other.gameObject.GetComponent<BaseBullets>()._ship_Shooter))
                    {
                        _enemy_List.Add(other.gameObject.GetComponent<BaseBullets>()._ship_Shooter);
                    }
                }
            }
        }

        
    }
}
