using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Radio : MonoBehaviour
{
    public List<AudioClip> _clipList = new List<AudioClip>();   //Lista de los clips de audio.
    private List<float> _audioClipTime = new List<float>(); //Tiempo que hay que esperar para que se vuelva a reproducir.
    private AudioSource _audioSource;

    void Start()
    {
       _audioSource = GetComponent<AudioSource>();
        for(int i = 0; i < _clipList.Count; i++)
        {
            _audioClipTime.Add(3f);
        }
    }

    void Update()
    {
       for(int i = 0; i < _audioClipTime.Count; i++)    //REcorro los clips de audio.
        {
            _audioClipTime[i] -= Time.deltaTime;    //Y voy restando el tiempo.
        }
    }


/*-------------------------------------------------------------------------------------
Todas las pistas de audio funcionan igual a la primera que voy a comentar.
*/
    public void Enemy_Spotted()
    {
        int Percentage = 0; //Porcentaje en que pueda sonar la pista
        Percentage = Random.Range(0, 101);  //Utilizo un numero al azar.
        for(int i = 0; i < _clipList.Count; i++)    //Recorro el array de la lista buscando el que necesito
        {//Si coincide el nombre de la pista y el porcentaje es menor al 20%
            if (_clipList[i].name == "Enemy Spotted" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f; //Y le seteo el tiempo de reproduccion para que no suene constantemente.
                _audioSource.PlayOneShot(_clipList[i]); //REporduzco el audio
            }
        }
    }

    public void Affirmative()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Affirmative" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Fall_Back()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Fall Back" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Follow_Me()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Follow Me" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Get_Down()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Get Down" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Good_Shoot()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Good Shoot" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Hold_This_Position()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Hold this Position" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Hit()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "I'm Hit!" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void In_Position()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "I'm in Position" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Out_Of_Here()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Lets get out of here" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Need_Backup()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Need Backup" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Not_Bad()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Not Bad" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Roger_That()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);

        int Select = Random.Range(0, 2);
        string Roger_Select = "";
        if(Select == 0)
        {
            Roger_Select = "Roger That";
        }
        else
        {
            Roger_Select = "Roger That 2";
        }
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == Roger_Select && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Run()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "RUN RUN RUN!" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void Teammate_Down()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Teammate Down" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }

    public void To_The_Shelter()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for (int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "To the Shelter, Now!" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
            }
        }
    }
}
