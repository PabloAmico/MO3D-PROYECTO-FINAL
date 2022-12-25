using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    ExplosionMissileZone _Explosion;

    float _time_Reset = 0.5f;
    bool _move = false;
    public bool in_Use = false;
    public GameObject _ship_Shooter;    //Nave que dispara el proyectil.
    protected int _Damage = 0;
    public SFX_Radio _radio;
    public Vector3 _pos_Objective;
    protected Vector3 _direction_Shoot;
    public float _velocity;

    /*protected override void Init()
    {
        _Explosion = GetComponentInChildren<ExplosionMissileZone>();
        _Explosion.GetComponent<SphereCollider>().enabled = false;
    }*/
    private void Start()
    {
        _Explosion = GetComponentInChildren<ExplosionMissileZone>();
        _Explosion.GetComponent<SphereCollider>().enabled = false;
        _radio = FindObjectOfType<SFX_Radio>();
    }

    public void Assign_PosAndRot(Vector3 pos, Quaternion angle, Vector3 posObj)
    {
        transform.position = pos;
        //transform.rotation = angle;
        _pos_Objective = posObj;
        _direction_Shoot = _pos_Objective;
        //print(transform.position);
        in_Use = true;
        gameObject.transform.LookAt(posObj);
    }

    private void Update()
    {
        Move_Bullet();
    }

    private void Move_Bullet()
    {
        if (gameObject.activeSelf)
        {
            //print("Moving!");
            in_Use = true;
            //            gameObject.GetComponent<Rigidbody>().AddForce(_direction_Shoot * _velocity, ForceMode.Impulse);

            transform.position = Vector3.MoveTowards(transform.position, _direction_Shoot, _velocity * Time.deltaTime);
            if (transform.position.x > _direction_Shoot.x - 0.5f && transform.position.x < _direction_Shoot.x + 0.5f)
            {
                if (transform.position.z > _direction_Shoot.z - 0.5f && transform.position.z < _direction_Shoot.z + 0.5f)
                {
                    transform.position = new Vector3(1000f, 1000f, 1000f);
                }
            }
        }
    }

    protected void Set_Texture()
     {

     }
    public void Set_Damage(int damage)
    {
        _Damage = damage;
    }

    public void Assign_Shooter(GameObject Shooter)
    {
        _ship_Shooter = Shooter;
        Set_Texture();
        /*if (_ship_Shooter.GetComponent<Faction>().Is_PlayerUnit)
        {
            gameObject.GetComponent<MeshRenderer>().material = _material_Bullet_Player;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = _material_Bullet_Enemy;
        }*/
    }

    private void Move_Missile()
    {
        if (_move)
        {
            _time_Reset -= Time.deltaTime;
            if(_time_Reset <= 0)
            {
                _time_Reset = 0.5f;
                _move = false;
                transform.position = new Vector3(1000f, 1000f, 1000f);
                OnBecameInvisible();
            }
        }
        
    }

    protected void OnBecameInvisible()
    {
        if (in_Use)
        {
            gameObject.SetActive(false);
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            in_Use = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship Enemy") && _ship_Shooter.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Colision Enemy");
            other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
            
            if (other != null)
            {
                other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente daño activo las particulas
            }

            //Onda expansiva del misil.
            _Explosion.Set_Shooter(_ship_Shooter);  //Se setea quien es el que disparó para poder distinguir los contrarios.
            _Explosion.Set_Objective(other.gameObject);
            _Explosion.GetComponent<SphereCollider>().enabled = true;  //Habilito la esfera de colision
            

            if (other.gameObject.GetComponent<StatsUnits>()._points_Life <= 0)
            {
                _ship_Shooter.GetComponent<StatsUnits>().Eliminate_Objective(other.gameObject.GetComponent<StatsUnits>());
                _radio.Get_Down();
                
            }
            Move_Missile();
            
        }

        if (other.gameObject != null && _ship_Shooter.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
            {
                //Debug.Log("Colision Player");
                other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
                other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente daño activo las particulas
                _Explosion.Set_Shooter(_ship_Shooter);  //Se setea quien es el que disparó para poder distinguir los contrarios.
                _Explosion.Set_Objective(other.gameObject);
                _Explosion.GetComponent<SphereCollider>().enabled = true;  //Habilito la esfera de colision
                

                if (other.gameObject.GetComponent<StatsUnits>()._points_Life <= 0)
                {
                    _ship_Shooter.GetComponent<StatsUnits>().Eliminate_Objective(other.gameObject.GetComponent<StatsUnits>());
                    //hacer lo mismo con las naves enemigas.
                   
                }
                //transform.position = new Vector3(1000f, 1000f, 1000f);
                Move_Missile();
            }
        }

        if (other.gameObject.CompareTag("Rocket") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
        {
            //print("Colision cohete");
            other.gameObject.GetComponent<Rocket>().Set_Damage(_Damage);
            transform.position = new Vector3(1000f, 1000f, 1000f);
            OnBecameInvisible();
        }
    }
    
}
