using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullets : MonoBehaviour
{
    public Vector3 _pos_Objective;
    public float _velocity;
    protected Vector3 _direction_Shoot;
    protected int _Damage= 0;
    public bool in_Use = false;
    public GameObject _ship_Shooter;    //Nave que dispara el proyectil.
    public Material _material_Bullet_Player;
    public Material _material_Bullet_Enemy;
    public SFX_Radio _radio;
    // Start is called before the first frame update
    void Start()
    {
        _radio = FindObjectOfType<SFX_Radio>();
        Init();
    }

    protected virtual void Init()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Move_Bullet();
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

    virtual protected void Set_Texture()
    {

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

    public void Set_Damage(int damage)
    {
        _Damage = damage;
    }


    protected void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.gameObject.CompareTag("Ship Enemy") && _ship_Shooter.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Colision Enemy");
                other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
                
                try
                {
                    if (other != null)
                    {
                        other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente daño activo las particulas
                    }
                }
                catch
                {

                }

                //Debug.Log(other.gameObject.GetComponent<StatsUnits>()._points_Life);
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

        if (other.gameObject != null && _ship_Shooter.gameObject != null)
        {
            if (other.gameObject.CompareTag("Player") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
            {
                //Debug.Log("Colision Player");
                other.gameObject.GetComponent<StatsUnits>().Set_Damage(_Damage);
                
                try
                {
                    other.gameObject.GetComponent<ParticleControlShips>().Active_Particles();    //si recibi suficiente daño activo las particulas
                                                                                                 //Debug.Log(other.gameObject.GetComponent<StatsUnits>()._points_Life);
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

        try
        {
            if (other != null)
            {
                if (other.gameObject.CompareTag("Rocket") && _ship_Shooter.gameObject.CompareTag("Ship Enemy"))
                {
                    //print("Colision cohete");
                    other.gameObject.GetComponent<Rocket>().Set_Damage(_Damage);
                    transform.position = new Vector3(1000f, 1000f, 1000f);
                }
            }
        }
        catch
        {

        }
        
    }

    
}
