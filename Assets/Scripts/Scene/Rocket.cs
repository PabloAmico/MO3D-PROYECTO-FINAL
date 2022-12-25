using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float _time_Complete;
    public float _time_current;
    private int _percentage = 0;
    public int _reset_Damage;
    private int _damage;
    public GameObject[] _Rocket_Parts;
    Canvas _canvas;
    public Image _fill_Image;
    private Text _text;
    ParticleSystem[] _particles;
    public List<GameObject> _enemy_List = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _text = GetComponentInChildren<Text>();
        _percentage = 0;
        foreach (GameObject part in _Rocket_Parts)
        {
            part.SetActive(false);
        }
        _Rocket_Parts[0].SetActive(true);
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
        {
            particle.Stop();
        }
    }

    // Update is called once per frame
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
       // _percentage = (int)((_time_current  * 100) / _time_Complete);
        if(_time_current >= _time_Complete / 100)
        {
            _time_current = 0;
            _percentage++;
        }
        //print(_percentage);
        if( _percentage > 0  && _percentage < 20)
        {
            if (!_Rocket_Parts[0].activeSelf)
            {
                _Rocket_Parts[0].SetActive(true);
            }
        }
        else
        {
            if(_percentage >= 20 && _percentage < 40)
            {
                if (!_Rocket_Parts[1].activeSelf)
                {
                    _Rocket_Parts[1].SetActive(true);
                }
            }
            else
            {
                if(_percentage >= 40 && _percentage < 60)
                {
                    if (!_Rocket_Parts[2].activeSelf)
                    {
                        _Rocket_Parts[2].SetActive(true);
                        
                    }
                   
                }
                else
                {
                    if(_percentage >= 60 && _percentage < 80)
                    {
                        if (!_Rocket_Parts[3].activeSelf)
                        {
                            _Rocket_Parts[3].SetActive(true);
                            
                            _particles[0].Play();
                        }
                        
                    }
                    else
                    {
                        if(_percentage >= 80 && _percentage < 100)
                        {
                            if (!_Rocket_Parts[4].activeSelf)
                            {
                                _Rocket_Parts[4].SetActive(true);
                                _particles[1].Play();
                            }
                        }
                        else
                        {
                            if(_percentage >= 100)
                            {
                                //Termina el juego
                            }
                        }
                    }
                }
            }
        }
    }

    public void Set_Damage(int Damage)
    {
        _damage += Damage;
        //print(_damage);
        if(_damage >= _reset_Damage)
        {
            //print(_time_current);
            _damage -= _reset_Damage; //Reseteo el damage y le asigno lo que sobra (por si el daño es mayor)

            _percentage -= 1;
            //print(_percentage);
        }
    }

    private void UI_Look_Camera()
    {
        _canvas.transform.forward = Camera.main.transform.forward;
        _fill_Image.fillAmount = (float)_percentage / 100;
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
                    //Debug.Log("bala al rocket");
                    // _damage += other.gameObject.GetComponent<Bullets>().
                }
            }
        }

        
    }
}
