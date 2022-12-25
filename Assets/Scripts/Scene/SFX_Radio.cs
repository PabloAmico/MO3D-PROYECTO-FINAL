using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Radio : MonoBehaviour
{
    public List<AudioClip> _clipList = new List<AudioClip>();
    private List<float> _audioClipTime = new List<float>();
    private AudioSource _audioSource;
   // private List<string> _clipNames = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
       _audioSource = GetComponent<AudioSource>();
        for(int i = 0; i < _clipList.Count; i++)
        {
            //print(_clipList[i].name);
            _audioClipTime.Add(3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < _audioClipTime.Count; i++)
        {
            _audioClipTime[i] -= Time.deltaTime;
        }
    }

    public void Enemy_Spotted()
    {
        int Percentage = 0;
        Percentage = Random.Range(0, 101);
        for(int i = 0; i < _clipList.Count; i++)
        {
            if (_clipList[i].name == "Enemy Spotted" && _audioClipTime[i] <= 0f && Percentage <= 20)
            {
                _audioClipTime[i] = 3f;
                _audioSource.PlayOneShot(_clipList[i]);
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
