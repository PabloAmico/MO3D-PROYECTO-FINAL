using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de la cual heredan las balas.
public class BaseBullets : MonoBehaviour
{
    public Vector3 _pos_Objective; //Posicion del objetivo.
    public float _velocity; //Velocidad de movimiento de la bala.
    protected Vector3 _direction_Shoot; //Direccion del disparo.
    protected int _Damage= 0;   
    public bool in_Use = false;
    public GameObject _ship_Shooter;    //Nave que dispara el proyectil.
    public SFX_Radio _radio;

    //La bala cambia de material al ser del jugador o del enemigo
    public Material _material_Bullet_Player;    
    public Material _material_Bullet_Enemy;




    void Start()
    {
        _radio = FindObjectOfType<SFX_Radio>();
        Init();
    }

    protected virtual void Init()
    {

    }
    void Update()
    {
        Move_Bullet();
    }


//Si las balas salen de la pantalla las cambio de posicion.
    protected void OnBecameInvisible()
    {
        if (in_Use) //Si estan en uso.
        {
            gameObject.SetActive(false);    //Las desactivo.
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            in_Use = false;
        }
    }


//Asigno la posicion y rotacion de la bala respecto de la nave que dispara.
    public void Assign_PosAndRot(Vector3 pos, Quaternion angle, Vector3 posObj)
    {
        transform.position = pos;
        _pos_Objective = posObj;
        _direction_Shoot = _pos_Objective;
        in_Use = true;
        gameObject.transform.LookAt(posObj);
    }


//Asigna la nave que dispara y dependiendo de eso cambia la textura.
    public void Assign_Shooter(GameObject Shooter)
    {
        _ship_Shooter = Shooter;
        Set_Texture();
    }

    virtual protected void Set_Texture()
    {

    }

//Metodo para mover la bala.
    private void Move_Bullet()
    {
        if (gameObject.activeSelf)  //Si esta activa
        {
           
            in_Use = true;
            //La muevo hacia la posicion del objetivo.
            transform.position = Vector3.MoveTowards(transform.position, _direction_Shoot, _velocity * Time.deltaTime); 
            //Si llego a la posicion
            if (transform.position.x > _direction_Shoot.x - 0.5f && transform.position.x < _direction_Shoot.x + 0.5f)
            {
                if (transform.position.z > _direction_Shoot.z - 0.5f && transform.position.z < _direction_Shoot.z + 0.5f)
                {
                    //La alejo y por ende se desactiva.
                    transform.position = new Vector3(1000f, 1000f, 1000f);
                }
            }
        }
    }

    public void Set_Damage(int damage)
    {
        _Damage = damage;
    }


    protected void OnTriggerEnter(Collider other)
    {

        //colision con la nave enemiga.
        try
        {
            if (other.gameObject.CompareTag("Ship Enemy") && _ship_Shooter.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
                
                try
                {
                    if (other != null)
                    {
                        other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente da�o activo las particulas
                    }
                }
                catch
                {

                }

                
                if (other.gameObject.GetComponent<StatsUnits>()._points_Life <= 0)
                {
                    _radio.Get_Down();
                    _ship_Shooter.GetComponent<StatsUnits>().Eliminate_Objective(other.gameObject.GetComponent<StatsUnits>());
                }
                transform.position = new Vector3(1000f, 1000f, 1000f);
            }
        }
        catch
        {

        }

        //colision con el jugador.
        if (other.gameObject != null && _ship_Shooter.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
            {
                other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
                
                try
                {
                    other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente da�o activo las particulas
                    if (other.gameObject.GetComponent<StatsUnits>()._points_Life <= 0)
                    {
                        _ship_Shooter.GetComponent<IA_Ship>()._units_InZone.Remove(other.gameObject.GetComponent<StatsUnits>());
                        _ship_Shooter.gameObject.GetComponent<IA_Ship>()._unit_Objective = null;
                    }
                }
                catch
                {

                }
               
                transform.position = new Vector3(1000f, 1000f, 1000f);
            }
        }


        //colision con cohete
        try
        {
            if (other != null)
            {
                if (other.gameObject.CompareTag("Rocket") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
                {
                    other.gameObject.GetComponent<Rocket>().Set_Damage(_Damage);
                    transform.position = new Vector3(1000f, 1000f, 1000f);
                }
            }
        }
        catch
        {

        }

        //colision con hangar enemigo
        try
        {
            if (other != null)
            {
                if (other.gameObject.CompareTag("HangarEnemy") && _ship_Shooter.gameObject.CompareTag("Player"))
                {
                    other.gameObject.GetComponent<Hangar_Enemy>().Set_Damage(_Damage);
                    transform.position = new Vector3(1000f, 1000f, 1000f);
                }
            }
        }
        catch
        {

        }
        
    }

    
}
